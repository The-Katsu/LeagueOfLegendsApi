using LeagueOfLegends.Api.Domain.Contracts.Responses.Region;
using LeagueOfLegends.Api.Domain.Entities;

namespace LeagueOfLegends.Api.Application.Mappers;

public static class RegionMapper
{
    private static RegionChampionResponse ToRegionChampionResponse(this Champion champion) =>
        new()
        {
            Id = champion.Id,
            Name = champion.Name
        };

    public static RegionResponseWithDetails ToRegionResponseWithDetails(this Region region) =>
        new()
        {
            Id = region.Id,
            Name = region.Name,
            ImageUrl = region.ImageUrl,
            AnimatedImageUrl = region.AnimatedImageUrl,
            Overview = region.Overview,
            AssociatedChampions = region.AssociatedChampions.Select(ToRegionChampionResponse).ToList()
        };

    public static RegionResponse ToRegionResponse(this Region region) =>
        new()
        {
            Id = region.Id,
            Name = region.Name,
            ImageUrl = region.ImageUrl
        };
}