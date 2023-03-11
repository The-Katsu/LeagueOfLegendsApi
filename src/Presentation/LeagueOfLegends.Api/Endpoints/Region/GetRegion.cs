﻿using LeagueOfLegends.Api.Application.Services.Interfaces;
using LeagueOfLegends.Api.Domain.Contracts.Requests;
using LeagueOfLegends.Api.Domain.Contracts.Responses;
using LeagueOfLegends.Api.Domain.Contracts.Responses.Region;
using LeagueOfLegends.Api.Domain.Contracts.Responses.Story;
using Microsoft.AspNetCore.Authorization;

namespace LeagueOfLegends.Api.Endpoints.Region;

[HttpGet("region/{id:int}"), AllowAnonymous]
public class GetRegion : Endpoint<GetByIdRequest, SingleResponse<RegionResponseWithDetails>>
{
    private readonly IRegionService _regionService;

    public GetRegion(IRegionService regionService) => _regionService = regionService;

    public override async Task HandleAsync(GetByIdRequest req, CancellationToken ct)
    {
        var response = await _regionService.GetByIdAsync(req.Id);

        if (response is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(response, ct);
    }
}