using LeagueOfLegends.Api.Application.Mappers;
using LeagueOfLegends.Api.Application.Services.Interfaces;
using LeagueOfLegends.Api.Domain.Contracts.Responses;
using LeagueOfLegends.Api.Domain.Contracts.Responses.Champion;
using LeagueOfLegends.Api.Infrastructure.Repositories.Interfaces;

namespace LeagueOfLegends.Api.Application.Services.Implementations;

public class ChampionService : IChampionService
{
    private readonly IChampionRepository _championRepository;
    
    public ChampionService(IChampionRepository championRepository) => _championRepository = championRepository;

    public async Task<ArrayResponse<ChampionResponse>> GetAllAsync()
    {
        var champions = await _championRepository.GetListAsync();
        var response = new ArrayResponse<ChampionResponse>(results: 
            champions
                .Select(ChampionMapper.ToChampionResponse)
                .ToList());
        return response;
    }

    public async Task<ArrayResponseWithInfo<ChampionResponse>> GetPageAsync(int page)
    {
        var champions = await _championRepository.GetChampionsPageAsync(page - 1);
        var count = await _championRepository.GetChampionsCountAsync();
        var pages = (int) Math.Ceiling(Convert.ToDecimal(count) / Convert.ToDecimal(20));
        var prev = page == 1 ? null : $"/api/champions/{page - 1}";
        var next = (page + 1) > pages ? null : $"/api/champions/{page + 1}";
        var response = new ArrayResponseWithInfo<ChampionResponse>
        {
            Info = new Information
            {
                Count = count,
                Pages = pages,
                Prev = prev!,
                Next = next!
            },
            Results = champions
                .Select(ChampionMapper.ToChampionResponse)
                .ToList()
        };
        return response;
    }

    public async Task<SingleResponse<ChampionResponseWithDetails>> GetByIdAsync(int id)
    {
        var champion = await _championRepository.GetByIdAsync(id);
        var response = champion is not null ? 
            new SingleResponse<ChampionResponseWithDetails>(result: champion.ToChampionResponseWithDetails()) :
            null;
        return response!;
    }
}