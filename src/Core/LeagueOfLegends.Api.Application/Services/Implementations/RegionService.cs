using LeagueOfLegends.Api.Application.Mappers;
using LeagueOfLegends.Api.Application.Services.Interfaces;
using LeagueOfLegends.Api.Domain.Contracts.Responses;
using LeagueOfLegends.Api.Domain.Contracts.Responses.Region;
using LeagueOfLegends.Api.Domain.Entities;
using LeagueOfLegends.Api.Infrastructure.Repositories.Interfaces;

namespace LeagueOfLegends.Api.Application.Services.Implementations;

public class RegionService : IRegionService
{
    private readonly IRegionRepository _regionRepository;

    public RegionService(IRegionRepository regionRepository) => _regionRepository = regionRepository;

    public IQueryable<Region> GetQuery() => _regionRepository.ProvideQueryable();

    public async Task<ArrayResponse<RegionResponse>> GetAllAsync()
    {
        var regions = await _regionRepository.GetListAsync();
        return new ArrayResponse<RegionResponse>(results: regions.Select(x => x.ToRegionResponse()).ToList());
    }

    public async Task<SingleResponse<RegionResponseWithDetails>> GetByIdAsync(int id)
    {
        var region = await _regionRepository.GetByIdAsync(id);
        var response = region is not null ? 
            new SingleResponse<RegionResponseWithDetails>(result: region.ToRegionResponseWithDetails()) : 
            null;
        return response!;
    }
}