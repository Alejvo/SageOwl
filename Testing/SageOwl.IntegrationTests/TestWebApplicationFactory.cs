using Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.MsSql;
using Testcontainers.Redis;

namespace SageOwl.IntegrationTests;

public class TestWebApplicationFactory
    : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _sqlContainer;
    private readonly RedisContainer _redisContainer;

    public TestWebApplicationFactory()
    {
        _sqlContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithPassword("YourStrong!Passw0rd")
            .Build();

        _redisContainer = new RedisBuilder()
            .WithImage("redis:7")
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _sqlContainer.StartAsync();
        await _redisContainer.StartAsync();

        using var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await db.Database.MigrateAsync();
        await TestDataSeeder.SeedAsync(scope.ServiceProvider);

    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _sqlContainer.DisposeAsync();
        await _redisContainer.DisposeAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureAppConfiguration((context, config) =>
        {
            var configuration = new Dictionary<string, string>
            {
                ["ConnectionStrings:DefaultConnection"] =
                    _sqlContainer.GetConnectionString(),

                ["Redis:ConnectionString"] =
                    _redisContainer.GetConnectionString(),
                ["JwtSettings:Key"] = "+08M7+wskskLtUMVsMQb3s8eKMbwqheJFaUdUXGaVwM=",
                ["JwtSettings:ExpiryTime"] = "15"
            };

            config.AddInMemoryCollection(configuration!);
        });

        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<AppDbContext>));

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(_sqlContainer.GetConnectionString());
            });
        });
    }
}