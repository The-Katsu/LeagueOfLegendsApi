using Quartz;

namespace LeagueOfLegends.Api.Persistence.Quartz.JobProviders;

public static class DynamicJobProvider
{
    public static async Task ProvideCronJob<T>(this IScheduler scheduler, string cron, 
        CancellationToken token = default) where T : IJob
    {
        var job = JobBuilder.Create<T>()
            .WithIdentity($"{typeof(T)}")
            .Build();

        var trigger = TriggerBuilder.Create()
            .WithIdentity($"{typeof(T)}")
            .WithCronSchedule(cron)
            .StartNow()
            .Build();

        await scheduler.ScheduleJob(job, trigger, token);
    }
}