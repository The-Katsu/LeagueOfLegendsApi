using LeagueOfLegends.Api.Application.Contracts.Requests;
using LeagueOfLegends.Api.Application.Contracts.Responses;
using LeagueOfLegends.Api.Application.Contracts.Responses.Champion;
using LeagueOfLegends.Api.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace LeagueOfLegends.Api.Endpoints.Champion;

[HttpGet("champion/{id:int}"), AllowAnonymous]
public class GetChampion : Endpoint<GetByIdRequest, SingleResponse<ChampionResponseWithDetails>>
{
    private readonly IChampionService _championService;

    public GetChampion(IChampionService championService) => _championService = championService;

    public override async Task HandleAsync(GetByIdRequest req, CancellationToken ct)
    {
        var response = await _championService.GetByIdAsync(req.Id);

        if (response is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(response, ct);
    }
}