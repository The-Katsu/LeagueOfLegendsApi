using LeagueOfLegends.Api.Application.Contracts.Responses.Champion;
using LeagueOfLegends.Api.Domain.Entities;

namespace LeagueOfLegends.Api.Application.Mappers;

public static class ChampionMapper
{
    private static ChampionAbilityResponse ToChampionAbilityResponse(this Ability ability) => 
        new()
        {
            Description = ability.Description,
            IconUrl = ability.IconUrl,
            Mp4Url = ability.Mp4Url,
            WebmUrl = ability.WebmUrl
        };

    private static ChampionRaceResponse ToChampionRaceResponse(this Race race) =>
        new()
        {
            Name = race.Name
        };

    private static ChampionSkinResponse ToChampionSkinResponse(this Skin skin) =>
        new()
        {
            Name = skin.Name,
            ImageUrl = skin.ImageUrl
        };

    private static ChampionRelatedChampionResponse
        ToChampionRelatedChampionResponse(this RelatedChampion relatedChampion) =>
        new()
        {
            Id = relatedChampion.Champion.Id,
            Name = relatedChampion.Champion.Name
        };

    private static ChampionRoleResponse ToChampionRoleResponse(this Role role) =>
        new()
        {
            Name = role.Name
        };

    public static ChampionResponseWithDetails ToChampionResponseWithDetails(this Champion champion) =>
        new()
        {
            Id = champion.Id,
            Name = champion.Name,
            Nickname = champion.Nickname,
            AnimatedImageUrl = champion.AnimatedImageUrl,
            ImageUrl = champion.ImageUrl,
            Biography = champion.Biography,
            ReleaseDate = champion.ReleaseDate,
            Roles = champion.Roles.Select(ToChampionRoleResponse).ToList(),
            Abilities = champion.Abilities.Select(ToChampionAbilityResponse).ToList(),
            Races = champion.Races.Select(ToChampionRaceResponse).ToList(),
            Skins = champion.Skins.Select(ToChampionSkinResponse).ToList(),
            RelatedChampions = champion.RelatedChampions.Select(ToChampionRelatedChampionResponse).ToList()
        };
    
    public static ChampionResponse ToChampionResponse(this Champion champion) =>
        new()
        {
            Id = champion.Id,
            Name = champion.Name,
            ImageUrl = champion.ImageUrl
        };
}