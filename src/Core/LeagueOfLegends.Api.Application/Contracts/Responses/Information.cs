namespace LeagueOfLegends.Api.Application.Contracts.Responses;

public class Information
{
    public int Count { get; set; }
    public int Pages { get; set; }
    public string? Next { get; set; } = null!;
    public string? Prev { get; set; } = null!;
}