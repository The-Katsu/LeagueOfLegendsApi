namespace LeagueOfLegends.Api.Domain.Contracts.Responses.Region;

public class RegionResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public string ImageUrl { get; init; } = null!;
}