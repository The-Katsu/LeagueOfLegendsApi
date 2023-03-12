using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace LeagueOfLegends.Api.Persistence.Quartz.Extensions;

public static class QuartzExtension
{
    public static IServiceCollection AddQuartz(this IServiceCollection services)
    {
        services.AddQuartz(q => q.UseMicrosoftDependencyInjectionJobFactory());

        services.AddQuartzServer(o => o.WaitForJobsToComplete = true);

        return services;
    }
}