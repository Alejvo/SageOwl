using Application.Interfaces;
using Domain.Announcements;
using Domain.Forms;
using Domain.Qualifications;
using Domain.Teams;
using Domain.Tokens;
using Domain.Users;
using Infrastructure.Auth;
using Infrastructure.Caching;
using Infrastructure.Messaging;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Infrastructure;

public static class DependencyInjection 
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opts =>opts.UseSqlServer(configuration.GetConnectionString("Default")));
        services.AddSingleton<IConnectionMultiplexer>(
            ConnectionMultiplexer.Connect(
                    configuration["Redis:ConnectionString"]
                ));

        services.AddScoped<IUserRepository,UserRepository>();
        services.AddScoped<IPasswordHasher,BCryptPasswordHasher>();
        services.AddScoped<ITokenRepository,TokenRepository>();
        services.AddScoped<ITeamRepository,TeamRepository>();
        services.AddScoped<IFormRepository,FormRepository>();
        services.AddScoped<IAnnouncementRepository,AnnouncementRepository>();
        services.AddScoped<IQualificationRepository,QualificationRepository>();
        services.AddScoped<IEmailService,EmailService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICacheService, RedisCacheService>();

        return services;
    }
}
