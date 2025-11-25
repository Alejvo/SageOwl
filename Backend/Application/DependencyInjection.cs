using Application.Abstractions;
using Application.Behaviors;
using Application.Users.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService,AuthService>();

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();
        });

        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>)
        );

        services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReference>();

        return services;
    }
}
