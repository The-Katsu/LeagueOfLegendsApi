using LeagueOfLegends.Api.Application.Jobs.Crawler;
using LeagueOfLegends.Api.Persistence.Quartz.JobProviders;
using Quartz;

namespace LeagueOfLegends.Api.Configuration;

public static class QuartzExtensions
{
    public static async Task StartQuartzJobs(this WebApplication webApplication)
    {
        using var scope = webApplication.Services.CreateScope();
        var scheduler = await scope.ServiceProvider.GetService<ISchedulerFactory>()!.GetScheduler();

        // run crawler job
        await scheduler.ProvideCronJob<CrawlerJob>("0 0 0 ? * * *"); // At 00:00:00am every day
    }
}