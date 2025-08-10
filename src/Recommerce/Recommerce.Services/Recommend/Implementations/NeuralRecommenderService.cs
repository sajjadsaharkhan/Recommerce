using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Recommerce.Data.DbContexts;
using Recommerce.Infrastructure.Rop;
using Recommerce.Services.Recommend.Dto;

namespace Recommerce.Services.Recommend.Implementations;

[UsedImplicitly]
public class NeuralRecommenderService : IRecommenderService
{
    private const string ModelRelativePath = "MLModels/recommenderModel.zip";
    private static readonly object ModelLock = new();
    private static ITransformer? _model;

    private readonly MLContext _mlContext = new();
    private readonly AppDbContext _dbContext;

    public NeuralRecommenderService(AppDbContext dbContext)
    {
        _dbContext = dbContext;

        if (_model is null)
        {
            lock (ModelLock)
            {
                _model ??= LoadOrTrainModel();
            }
        }
    }

    public async Task<Result<IList<string>>> GetRecommendationAsync(
        RecommendationInDto recommendationInDto,
        CancellationToken cancellationToken)
    {
        var customerId = await _dbContext.Customers
            .Where(c => c.UniqueIdentifier == recommendationInDto.CustomerIdentifier)
            .Select(c => c.Id)
            .SingleAsync(cancellationToken);

        var purchasedProducts = await _dbContext.Orders
            .Where(o => o.CustomerId == customerId)
            .Join(_dbContext.Products, o => o.ProductId, p => p.Id,
                (o, p) => p.UniqueIdentifier)
            .Distinct()
            .ToListAsync(cancellationToken);

        var allProducts = await _dbContext.Products
            .Select(p => p.UniqueIdentifier)
            .ToListAsync(cancellationToken);

        var predictionEngine = _mlContext.Model
            .CreatePredictionEngine<OrderEntry, ProductScore>(_model!);

        var scoredProducts = allProducts
            .Select(pid => new
            {
                ProductId = pid,
                Score = predictionEngine.Predict(new OrderEntry
                {
                    UserId = (uint)customerId,
                    ProductId = pid,
                    Label = 1f
                }).Score
            });

        if (recommendationInDto.PreventRepetitiveProducts)
        {
            scoredProducts = scoredProducts
                .Where(x => !purchasedProducts.Contains(x.ProductId));
        }

        var recommendations = scoredProducts
            .OrderByDescending(x => x.Score)
            .Select(x => x.ProductId)
            .Take(recommendationInDto.ProductCount)
            .ToList();

        return Result<IList<string>>.Success(recommendations);
    }

    private ITransformer LoadOrTrainModel()
    {
        var modelPath = Path.Combine(AppContext.BaseDirectory, ModelRelativePath);

        if (File.Exists(modelPath))
        {
            using var stream = File.OpenRead(modelPath);
            return _mlContext.Model.Load(stream, out _);
        }

        var trainingData = _dbContext.Orders
            .Join(_dbContext.Products, o => o.ProductId, p => p.Id,
                (o, p) => new OrderEntry
                {
                    UserId = (uint)o.CustomerId,
                    ProductId = p.UniqueIdentifier,
                    Label = 1f
                })
            .ToList();

        var dataView = _mlContext.Data.LoadFromEnumerable(trainingData);

        var estimator = _mlContext.Transforms.Conversion
                .MapValueToKey("UserIdEncoded", nameof(OrderEntry.UserId))
            .Append(_mlContext.Transforms.Conversion
                .MapValueToKey("ProductIdEncoded", nameof(OrderEntry.ProductId)))
            .Append(_mlContext.Recommendation().Trainers.MatrixFactorization(
                labelColumnName: nameof(OrderEntry.Label),
                matrixRowIndexColumnName: "UserIdEncoded",
                matrixColumnIndexColumnName: "ProductIdEncoded"));

        var model = estimator.Fit(dataView);

        Directory.CreateDirectory(Path.GetDirectoryName(modelPath)!);
        using var fileStream = File.Create(modelPath);
        _mlContext.Model.Save(model, dataView.Schema, fileStream);

        return model;
    }

    private sealed class OrderEntry
    {
        public uint UserId { get; set; }
        public string ProductId { get; set; } = string.Empty;
        public float Label { get; set; }
    }

    private sealed class ProductScore
    {
        public float Score { get; set; }
    }
}

