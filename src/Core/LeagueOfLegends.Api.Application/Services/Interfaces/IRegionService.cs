using LeagueOfLegends.Api.Domain.Contracts.Responses;
using LeagueOfLegends.Api.Domain.Contracts.Responses.Region;

namespace LeagueOfLegends.Api.Application.Services.Interfaces;

public interface IRegionService
{
    public Task<ArrayResponse<RegionResponse>> GetAllAsync();

    public Task<SingleResponse<RegionResponseWithDetails>> GetByIdAsync(int id);
}