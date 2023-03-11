using LeagueOfLegends.Api.Application.Services.Interfaces;
using LeagueOfLegends.Api.Domain.Contracts.Responses;
using LeagueOfLegends.Api.Domain.Contracts.Responses.Region;
using LeagueOfLegends.Api.Domain.Contracts.Responses.Story;
using LeagueOfLegends.Api.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace LeagueOfLegends.Api.Endpoints.Region;

[HttpGet("region"), AllowAnonymous]
public class GetRegions : EndpointWithoutRequest<ArrayResponse<RegionResponse>>
{
    private readonly IRegionService _regionService;

    public GetRegions(IRegionService regionService) => _regionService = regionService;

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await _regionService.GetAllAsync();
        await SendOkAsync(response, ct);
    }
}