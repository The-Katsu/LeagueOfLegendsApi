using LeagueOfLegends.Api.Application.Contracts.Responses;
using LeagueOfLegends.Api.Application.Contracts.Responses.Champion;
using LeagueOfLegends.Api.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace LeagueOfLegends.Api.Endpoints.Champion;

[HttpGet("champion"), AllowAnonymous]
public class GetChampions : EndpointWithoutRequest<ArrayResponse<ChampionResponse>>
{
    private readonly IChampionService _championService;

    public GetChampions(IChampionService championService) => _championService = championService;

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await _championService.GetAllWithDetailsAsync();
        await SendOkAsync(response, ct);
    }
}