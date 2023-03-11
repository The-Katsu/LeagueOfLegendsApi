namespace LeagueOfLegends.Api.Application.Jobs.Crawler.Parser.Constants;

public static class Uris
{
    public static readonly Uri AllUniverseChampionsUrl = 
        new("https://universe-meeps.leagueoflegends.com/v1/en_gb/search/index.json");

    public static readonly Uri AllGameChampionUrl =
        new("https://www.leagueoflegends.com/page-data/en-gb/champions/page-data.json");
    
    public static Uri UniverseChampionUrl(string name) => 
        new($"https://universe-meeps.leagueoflegends.com/v1/en_gb/champions/{name}/index.json");

    public static Uri GameChampionUrl(string name) =>
        new($"https://www.leagueoflegends.com/page-data/en-gb/champions/{name}/page-data.json");

    public static readonly Uri ExploreUniverseUrl =
        new("https://universe-meeps.leagueoflegends.com/v1/en_gb/explore2/index.json");

    public static Uri ExplorerItemUri(string contentUri) =>
        new($"https://universe-meeps.leagueoflegends.com/v1{contentUri}/index.json");

    public static string ExplorerItemUriAtSite(string contentUri) =>
        $"https://universe.leagueoflegends.com{contentUri}";

    public static Uri ComicUri(string name) =>
        new($"https://universe-comics.leagueoflegends.com/comics{name}/index.json");

    public static string ComicSiteUrl(string name) =>
        $"https://universe.leagueoflegends.com{name}";

    public static Uri RegionsUri =
        new("https://universe-meeps.leagueoflegends.com/v1/en_gb/faction-browse/index.json");

    public static Uri RegionUri(string name) =>
        new($"https://universe-meeps.leagueoflegends.com/v1/en_gb/factions/{name}/index.json");
}