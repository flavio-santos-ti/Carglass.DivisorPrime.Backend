using Carglass.DivisorPrime.Mappers.Builders;
using Carglass.DivisorPrime.Mappers.Interfaces;
using Carglass.DivisorPrime.Service.Handlers;
using Carglass.DivisorPrime.Service.Interfaces;
using Carglass.DivisorPrime.Service.Services;
using Carglass.DivisorPrime.Service.Validators;
using System.Diagnostics.CodeAnalysis;

namespace Carglass.DivisorPrime.Api.Configuration;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddHandlers();
        services.AddServices();
        services.AddBuilders();
        services.AddValidators();

        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IDivisorHandler, DivisorHandler>();
        services.AddScoped<IPrimeDivisorHandler, PrimeDivisorHandler>();
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IDivisorService, DivisorService>();
        return services;
    }

    private static IServiceCollection AddBuilders(this IServiceCollection services)
    {
        services.AddScoped<IResponseBuilder, ResponseBuilder>();
        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<INumberValidator, NumberValidator>();
        return services;
    }
}
