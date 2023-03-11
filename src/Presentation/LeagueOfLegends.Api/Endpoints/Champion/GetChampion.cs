using LeagueOfLegends.Api.Application.Services.Interfaces;
using LeagueOfLegends.Api.Domain.Contracts.Requests;
using LeagueOfLegends.Api.Domain.Contracts.Responses;
using LeagueOfLegends.Api.Domain.Contracts.Responses.Champion;
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