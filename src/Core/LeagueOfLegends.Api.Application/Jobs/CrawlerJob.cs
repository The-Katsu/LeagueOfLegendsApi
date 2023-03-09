using LeagueOfLegends.Api.Application.Parser;
using Quartz;

namespace LeagueOfLegends.Api.Application.Jobs;

[DisallowConcurrentExecution]
public sealed class CrawlerJob : IJob
{
    private readonly ILolParser _lolParser;

    public CrawlerJob(ILolParser lolParser) => _lolParser = lolParser;

    public async Task Execute(IJobExecutionContext context)
    {
        await _lolParser.ParseChampions();
    }
}