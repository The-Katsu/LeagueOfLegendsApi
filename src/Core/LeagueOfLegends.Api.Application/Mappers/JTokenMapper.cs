using LeagueOfLegends.Api.Application.Parser.Extensions;
using LeagueOfLegends.Api.Application.Utils;
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
            return token.Value<DateTime>();
        }
        catch
        {
            return new DateTime();
        }
    }
    
    public static Champion MapChampion(JToken universeToken) =>
        new()
        {
            Name = TryGetString(universeToken,new [] {"name"}),
            Nickname = TryGetString(universeToken, new[] {"title"}),
            ImageUrl = TryGetString(universeToken, new[] {"image","uri"}),
            AnimatedImageUrl = TryGetString(universeToken, new[] {"video","uri"}),
            Biography = TryGetString(universeToken, new[] {"biography","full"}) is not null ?
                HtmlUtility.GetPlaintText(TryGetString(universeToken, new[] {"biography","full"})) :
                null!,
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
}