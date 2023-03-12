using Microsoft.Extensions.DependencyInjection;

namespace LeagueOfLegends.Api.Infrastructure.Extensions;

public static class LazyServiceExtension
{
    public static IServiceCollection AllowLazy<T>(this IServiceCollection services)
    {
        var service = services.Last();
        var descriptor = new ServiceDescriptor(
            typeof(Lazy<T>),
            provider => new Lazy<T>(provider.GetService<T>()!),
            service.Lifetime
        );
        services.Add(descriptor);
        return services;
    }
}