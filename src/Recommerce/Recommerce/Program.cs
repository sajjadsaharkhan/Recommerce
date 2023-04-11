using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Recommerce.Data.DbContexts;
using Recommerce.Infrastructure.Extensions;

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



var app = builder.Build();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseGeneralServices();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();