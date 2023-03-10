using LeagueOfLegends.Api.Application.Contracts.Requests.Champion;
using LeagueOfLegends.Api.Application.Contracts.Responses;
using LeagueOfLegends.Api.Application.Contracts.Responses.Champion;
using LeagueOfLegends.Api.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace LeagueOfLegends.Api.Endpoints.Champion;

[HttpGet("/api/champions/{pageNum:int}"), AllowAnonymous]
public class GetChampionsPage : Endpoint<GetChampionsPageRequest, ArrayResponse<ChampionResponse>>
{
    private readonly IChampionService _championService;

    public GetChampionsPage(IChampionService championService) => _championService = championService;

    public override async Task HandleAsync(GetChampionsPageRequest req, CancellationToken ct)
    {
        var response = await _championService.GetPageAsync(req.PageNum);

        if (response.Results.Count == 0)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(response, ct);
    }
}