using LeagueOfLegends.Api.Application.Contracts.Responses.Champion;
using LeagueOfLegends.Api.Domain.Entities;

namespace LeagueOfLegends.Api.Application.Mappers;

public static class ChampionMapper
{
    public static ChampionAbilityResponse ToChampionAbilityResponse(this Ability ability) => 
        new()
        {
            Id = ability.Id,
            Description = ability.Description,
            IconUrl = ability.IconUrl,
            Mp4Url = ability.Mp4Url,
            WebmUrl = ability.WebmUrl
        };

    public static ChampionRaceResponse ToChampionRaceResponse(this Race race) =>
        new()
        {
            Id = race.Id,
            Name = race.Name
        };

    public static ChampionSkinResponse ToChampionSkinResponse(this Skin skin) =>
        new()
        {
            Id = skin.Id,
            Name = skin.Name,
            ImageUrl = skin.ImageUrl
        };

    public static ChampionRelatedChampionResponse
        ToChampionRelatedChampionResponse(this RelatedChampion relatedChampion) =>
        new()
        {
            Id = relatedChampion.Champion.Id,
            Name = relatedChampion.Champion.Name
        };

    public static ChampionResponse ToChampionResponse(this Champion champion) =>
        new()
        {
            Id = champion.Id,
            Name = champion.Name,
            Nickname = champion.Nickname,
            AnimatedImageUrl = champion.AnimatedImageUrl,
            ImageUrl = champion.ImageUrl,
            Biography = champion.Biography,
            ReleaseDate = champion.ReleaseDate,
            Abilities = champion.Abilities.Select(ToChampionAbilityResponse).ToList(),
            Races = champion.Races.Select(ToChampionRaceResponse).ToList(),
            Skins = champion.Skins.Select(ToChampionSkinResponse).ToList(),
            RelatedChampions = champion.RelatedChampions.Select(ToChampionRelatedChampionResponse).ToList()
        };
}