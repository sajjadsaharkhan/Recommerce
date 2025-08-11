using System;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Recommerce.Data.DbContexts;
using Recommerce.Infrastructure.Extensions;
using Recommerce.Services.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.RegisterGeneralServices();

builder.Services.AddDbContextPool<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Recommerce");
    ArgumentNullException.ThrowIfNull(connectionString);

    options.UseSqlServer(connectionString, ss =>
    {
        ss.MigrationsAssembly("Recommerce.Data");
        ss.EnableRetryOnFailure(3);
    });
});

builder.Services.AddMassTransit(x =>
{
    x.AddConsumers(typeof(UserCreatedConsumer).Assembly);

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.UseGeneralServices();

app.Run();