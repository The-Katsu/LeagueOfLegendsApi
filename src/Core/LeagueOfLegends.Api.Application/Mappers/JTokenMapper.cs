using LeagueOfLegends.Api.Application.Jobs.Crawler.Parser.Constants;
using LeagueOfLegends.Api.Application.Jobs.Crawler.Parser.Extensions;
using LeagueOfLegends.Api.Domain.Entities;
using Newtonsoft.Json.Linq;

namespace LeagueOfLegends.Api.Application.Mappers;

public static class JTokenMapper
{
    private static string TryGetString(JToken token, IEnumerable<string> childesChain)
    {
        try
        {
            token = childesChain.Aggregate(token, (current, child) => current[child]!);
            return token.GetStringValue();
        }
        catch
        {
            return null!;
        }
    }
    
    private static DateTime TryGetDateTime(JToken token, IEnumerable<string> childesChain)
    {
        try
        {
            token = childesChain.Aggregate(token, (current, child) => current[child]!);
            return token.Value<DateTime>().ToUniversalTime();
        }
        catch
        {
            return new DateTime().ToUniversalTime();
        }
    }

    private static int TryGetInt(JToken token, IEnumerable<string> childesChain)
    {
        try
        {
            token = childesChain.Aggregate(token, (current, child) => current[child]!);
            return token.Value<int>();
        }
        catch
        {
            return 0;
        }
    }
    
    public static Champion MapChampion(JToken universeToken) =>
        new()
        {
            Name = TryGetString(universeToken,new [] {"name"}),
            Nickname = TryGetString(universeToken, new[] {"title"}),
            ImageUrl = TryGetString(universeToken, new[] {"image","uri"}),
            AnimatedImageUrl = TryGetString(universeToken, new[] {"video","uri"}),
            Biography = TryGetString(universeToken, new[] {"biography","full"}),
            ReleaseDate = TryGetDateTime(universeToken, new []{"release-date"})
        };

    public static Ability MapPassiveAbility(JToken gameToken) =>
        new()
        {
            Description = TryGetString(gameToken, new[] {"champion_passive", "champion_passive_description"}),
            IconUrl = TryGetString(gameToken, new[] {"champion_passive", "champion_passive_icon"}),
            Name = TryGetString(gameToken, new[] {"champion_passive", "champion_passive_name"}),
            Mp4Url = TryGetString(gameToken, new[] {"champion_passive", "champion_passive_video_mp4"}),
            WebmUrl = TryGetString(gameToken, new[] {"champion_passive", "champion_passive_video_webm"})
        };
    
    public static Ability MapQAbility(JToken gameToken) =>
        new()
        {
            Description = TryGetString(gameToken, new[] {"champion_q", "champion_q_description"}),
            IconUrl = TryGetString(gameToken, new[] {"champion_q", "champion_q_icon"}),
            Name = TryGetString(gameToken, new[] {"champion_q", "champion_q_name"}),
            Mp4Url = TryGetString(gameToken, new[] {"champion_q", "champion_q_video_mp4"}),
            WebmUrl = TryGetString(gameToken, new[] {"champion_q", "champion_q_video_webm"})
        };
    
    public static Ability MapWAbility(JToken gameToken) =>
        new()
        {
            Description = TryGetString(gameToken, new[] {"champion_w", "champion_w_description"}),
            IconUrl = TryGetString(gameToken, new[] {"champion_w", "champion_w_icon"}),
            Name = TryGetString(gameToken, new[] {"champion_w", "champion_w_name"}),
            Mp4Url = TryGetString(gameToken, new[] {"champion_w", "champion_w_video_mp4"}),
            WebmUrl = TryGetString(gameToken, new[] {"champion_w", "champion_w_video_webm"})
        };
    
    public static Ability MapEAbility(JToken gameToken) =>
        new()
        {
            Description = TryGetString(gameToken, new[] {"champion_e", "champion_e_description"}),
            IconUrl = TryGetString(gameToken, new[] {"champion_e", "champion_e_icon"}),
            Name = TryGetString(gameToken, new[] {"champion_e", "champion_e_name"}),
            Mp4Url = TryGetString(gameToken, new[] {"champion_e", "champion_e_video_mp4"}),
            WebmUrl = TryGetString(gameToken, new[] {"champion_e", "champion_e_video_webm"})
        };
    
    public static Ability MapRAbility(JToken gameToken) =>
        new()
        {
            Description = TryGetString(gameToken, new[] {"champion_r", "champion_r_description"}),
            IconUrl = TryGetString(gameToken, new[] {"champion_r", "champion_r_icon"}),
            Name = TryGetString(gameToken, new[] {"champion_r", "champion_r_name"}),
            Mp4Url = TryGetString(gameToken, new[] {"champion_r", "champion_r_video_mp4"}),
            WebmUrl = TryGetString(gameToken, new[] {"champion_r", "champion_r_video_webm"})
        };

    public static Skin MapSkin(JToken gameToken) =>
        new()
        {
            Name = TryGetString(gameToken, new[] {"name"}),
            ImageUrl = TryGetString(gameToken, new[] {"imageUrl"}),
        };

    public static Role MapRole(JToken universeToken) =>
        new()
        {
            Name = TryGetString(universeToken, new []{"name"})
        };
    
    public static Race MapRace(JToken universeToken) =>
        new()
        {
            Name = TryGetString(universeToken, new []{"name"})
        };

    public static Story MapStory(JToken token, JObject jObject) =>
        new()
        {
            ReleaseDate = TryGetDateTime(token, new[] {"release-date"}),
            Title = TryGetString(token, new[] {"title"}),
            WordCount = TryGetInt(jObject.ToObject<JToken>()!, new[] {"word-count"}),
            Url = Uris.ExplorerItemUriAtSite(TryGetString(token, new[] {"url"}) ?? ""),
            MinutesToRead = TryGetInt(token, new[] {"minutes-to-read"}),
            Subtitle = TryGetString(token,new[] {"subtitle"}),
            ImageUrl = TryGetString(token, new[] {"featured-image","uri"}),
            Content = ""
        };

    public static Video MapVideo(JToken token) =>
        new()
        {
            Title = token["title"]!.GetStringValue(),
            Description = token["description"]!.GetStringValue(),
            ImageUrl = token["featured-image"]!["uri"]!.GetStringValue(),
            Url = token["uri"]!.GetStringValue(),
            Type = token["type"]!.GetStringValue()
        };

    public static Comic MapComic(JToken token) => 
        new()
        {
            Title = TryGetString(token, new[] {"title"}),
            Url = Uris.ComicSiteUrl(TryGetString(token, new [] {"url"}) ?? ""),
            Credits = "",
            Content = "",
            Description = TryGetString(token, new[] {"description"})
        };

    public static Region MapRegion(JToken token) =>
        new()
        {
            Name = TryGetString(token,new [] {"name"}),
            ImageUrl = TryGetString(token, new []{"image","uri"}),
            AnimatedImageUrl = TryGetString(token, new [] {"video","uri"}),
            Overview = TryGetString(token, new[] {"overview","short"})
        };
}