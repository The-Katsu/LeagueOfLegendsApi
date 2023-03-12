namespace LeagueOfLegends.Api.Domain.Contracts.Responses.Region;

public class RegionResponseWithDetails
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public string ImageUrl { get; init; } = null!;
    public string AnimatedImageUrl { get; init; } = null!;
    public string Overview { get; init; } = null!;
    public IList<RegionChampionResponse> AssociatedChampions { get; init; } = new List<RegionChampionResponse>();
}