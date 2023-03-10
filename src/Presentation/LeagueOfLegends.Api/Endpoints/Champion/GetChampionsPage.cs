﻿using LeagueOfLegends.Api.Application.Contracts.Requests;
using LeagueOfLegends.Api.Application.Contracts.Requests.Champion;
using LeagueOfLegends.Api.Application.Contracts.Responses;
using LeagueOfLegends.Api.Application.Contracts.Responses.Champion;
using LeagueOfLegends.Api.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace LeagueOfLegends.Api.Endpoints.Champion;

[HttpGet("champion/page-{page:int}"), AllowAnonymous]
public class GetChampionsPage : Endpoint<GetChampionsByPageRequest, ArrayResponseWithInfo<ChampionResponse>>
{
    private readonly IChampionService _championService;

    public GetChampionsPage(IChampionService championService) => _championService = championService;

    public override async Task HandleAsync(GetChampionsByPageRequest req, CancellationToken ct)
    {
        var response = await _championService.GetPageWithDetailsAsync(req.Page); // well, let it be page id :)

        if (response.Results.Count == 0)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(response, ct);
    }
}