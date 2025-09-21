using Domain.Announcements;
using Domain.Forms;
using Domain.Qualifications;
using Domain.Teams;
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
        services.AddScoped<ITeamRepository,TeamRepository>();
        services.AddScoped<IFormRepository,FormRepository>();
        services.AddScoped<IAnnouncementRepository,AnnouncementRepository>();
        services.AddScoped<IQualificationRepository,QualificationRepository>();
        return services;
    }
}
