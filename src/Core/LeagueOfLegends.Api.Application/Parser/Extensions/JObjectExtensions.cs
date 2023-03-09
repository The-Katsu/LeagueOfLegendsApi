using Newtonsoft.Json.Linq;

namespace LeagueOfLegends.Api.Application.Parser.Extensions;

public static class JObjectExtensions
{
    public static JToken GetGameChampionNodes(this JObject token) =>
        token["result"]!["data"]!["all"]!["nodes"]!.First!;
    
    public static JToken GetUniverseChampionNodes(this JObject token) =>
        token["champion"]!;
}