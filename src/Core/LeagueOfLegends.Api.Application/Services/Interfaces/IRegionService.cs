using LeagueOfLegends.Api.Domain.Contracts.Responses;
using LeagueOfLegends.Api.Domain.Contracts.Responses.Region;
using LeagueOfLegends.Api.Domain.Entities;

namespace LeagueOfLegends.Api.Application.Services.Interfaces;

public interface IRegionService
{
    public IQueryable<Region> GetQuery();
    public Task<ArrayResponse<RegionResponse>> GetAllAsync();

    public Task<SingleResponse<RegionResponseWithDetails>> GetByIdAsync(int id);
}