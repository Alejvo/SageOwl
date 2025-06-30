using Domain.Tokens;
using Domain.Users;
using Infrastructure.Contexts;
using Infrastructure.PasswordHasher;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection 
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opts =>opts.UseSqlServer(configuration.GetConnectionString("Default")));
        services.AddScoped<IUserRepository,UserRepository>();
        services.AddScoped<IPasswordHasher,BCryptPasswordHasher>();
        services.AddScoped<ITokenRepository,TokenRepository>();
        return services;
    }
}
