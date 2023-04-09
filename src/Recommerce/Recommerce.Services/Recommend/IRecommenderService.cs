using Recommerce.Infrastructure.Rop;
using Recommerce.Services.Recommend.Dto;
using Scrutor.AspNetCore;

namespace Recommerce.Services.Recommend;

public interface IRecommenderService : IScopedLifetime
{
    public Task<Result<IList<string>>> GetRecommendationAsync(RecommendationInDto recommendationInDto,
        CancellationToken cancellationToken);
}