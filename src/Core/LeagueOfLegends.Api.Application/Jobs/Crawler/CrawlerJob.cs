using LeagueOfLegends.Api.Application.Jobs.Crawler.Parser;
using Quartz;

namespace LeagueOfLegends.Api.Application.Jobs.Crawler;

[DisallowConcurrentExecution]
public sealed class CrawlerJob : IJob
{
    private readonly ILolParser _lolParser;

    public CrawlerJob(ILolParser lolParser) => _lolParser = lolParser;

    public async Task Execute(IJobExecutionContext context)
    {
        await _lolParser.ParseChampions();
        await _lolParser.ParseUniverse();
    }
}