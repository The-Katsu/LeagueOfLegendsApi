using LeagueOfLegends.Api.Application.Mappers;
using LeagueOfLegends.Api.Application.Services.Interfaces;
using LeagueOfLegends.Api.Domain.Contracts.Responses;
using LeagueOfLegends.Api.Domain.Contracts.Responses.Champion;
using LeagueOfLegends.Api.Domain.Entities;
using LeagueOfLegends.Api.Domain.Exceptions;
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
        if (champions.Count == 0) throw new PageNotFoundException(typeof(Champion), page);
        var count = await _championRepository.GetChampionsCountAsync();
        var pages = (int) Math.Ceiling(Convert.ToDecimal(count) / 20.0m);
        return new ArrayResponseWithInfo<ChampionResponse>
        {
            Info = new Information
            {
                TotalCount = count,
                Pages = pages,
                Prev = page == 1 ? null : $"/api/champions/{page - 1}",
                Next = page + 1 > pages ? null : $"/api/champions/{page + 1}"
            },
            Results = champions
                .Select(ChampionMapper.ToChampionResponse)
                .ToList()
        };
    }

    public async Task<SingleResponse<ChampionResponseWithDetails>> GetByIdAsync(int id)
    {
        var champion = await _championRepository.GetByIdAsync(id);
        if (champion is null) throw new EntityNotFoundException(typeof(Champion), id);
        return new SingleResponse<ChampionResponseWithDetails>(champion.ToChampionResponseWithDetails());
    }
}