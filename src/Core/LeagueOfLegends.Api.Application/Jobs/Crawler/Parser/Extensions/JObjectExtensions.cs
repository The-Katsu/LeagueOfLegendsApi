using Newtonsoft.Json.Linq;

namespace LeagueOfLegends.Api.Application.Jobs.Crawler.Parser.Extensions;

public static class JObjectExtensions
{
    public static JToken GetGameChampionNodes(this JObject jObject) =>
        jObject["result"]!["data"]!["all"]!["nodes"]!.First!;
    
    public static JToken GetUniverseChampionNodes(this JObject jObject) =>
        jObject["champion"]!;

    public static JToken GetUniverseExploreModules(this JObject jObject) =>
        jObject["modules"]!;
}