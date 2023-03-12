using Newtonsoft.Json.Linq;

namespace LeagueOfLegends.Api.Application.Jobs.Crawler.Parser.Extensions;

public static class JTokenExtensions
{
    public static JToken GetGameChampions(this JToken token) => 
        token["result"]!["data"]!["allChampions"]!["edges"]!;

    public static JToken GetUniverseChampions(this JToken token) => 
        token["champions"]!;

    public static string GameChampionNameWithoutSeparators(this JToken token) =>
        new(token["node"]!["champion_name"]!.GetStringValue().Where(char.IsLetter).ToArray());
    
    public static string UniverseChampionNameWithoutSeparators(this JToken token) =>
        new(token["name"]!.GetStringValue().Where(char.IsLetter).ToArray());

    public static string GameChampionUrlName(this JToken token) => 
        token["node"]!["url"]!.GetStringValue().Split("/").Last(x => !string.IsNullOrWhiteSpace(x));

    public static string UniverseChampionUrlName(this JToken token) => 
        token["slug"]!.GetStringValue();

    public static string GetStringValue(this JToken token) =>
        token.Value<string>()!;

    public static JToken GetRelatedChampions(this JToken token) =>
        token.Parent!.Parent!["related-champions"]!;

    public static string GetRelatedChampionName(this JToken token) =>
        token["name"]!.GetStringValue();

    public static string GetExploreItemType(this JToken token) =>
        token["type"]!.GetStringValue();
    
}