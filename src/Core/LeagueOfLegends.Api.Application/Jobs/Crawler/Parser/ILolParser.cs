namespace LeagueOfLegends.Api.Application.Jobs.Crawler.Parser;

public interface ILolParser
{
    public Task ParseChampions();
    public Task ParseUniverse();
    public Task ParseRegions();
}