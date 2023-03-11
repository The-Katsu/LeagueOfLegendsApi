namespace LeagueOfLegends.Api.Domain.Contracts.Responses;

public class Information
{
    public int Count { get; set; }
    public int Pages { get; set; }
    public string? Next { get; set; }
    public string? Prev { get; set; }
}