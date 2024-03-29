﻿namespace LeagueOfLegends.Api.Domain.Contracts.Responses.Champion;

public class ChampionResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public string ImageUrl { get; init; } = null!;
}