namespace LeagueOfLegends.Api.Application.Parser.Constants;

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
}