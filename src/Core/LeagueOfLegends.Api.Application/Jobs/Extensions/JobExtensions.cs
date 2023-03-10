using LeagueOfLegends.Api.Persistence.Quartz.JobProviders;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace LeagueOfLegends.Api.Application.Jobs.Extensions;

public static class JobExtensions
{
    public static async Task StartQuartzJobs(this WebApplication webApplication)
    {
        using var scope = webApplication.Services.CreateScope();
        var scheduler = await scope.ServiceProvider.GetService<ISchedulerFactory>()!.GetScheduler();
        
        // run crawler job
        await scheduler.ProvideCronJob<CrawlerJob>("0 0 0 ? * * *"); // At 00:00:00am every day
    }
}