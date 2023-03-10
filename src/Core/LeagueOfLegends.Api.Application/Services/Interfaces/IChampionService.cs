using LeagueOfLegends.Api.Application.Contracts.Responses;
using LeagueOfLegends.Api.Application.Contracts.Responses.Champion;

namespace LeagueOfLegends.Api.Application.Services.Interfaces;

public interface IChampionService
{
    public Task<ArrayResponse<ChampionResponse>> GetAllWithDetailsAsync();
    public Task<ArrayResponseWithInfo<ChampionResponse>> GetPageWithDetailsAsync(int page);

    public Task<SingleResponse<ChampionResponseWithDetails>> GetByIdAsync(int id);
}