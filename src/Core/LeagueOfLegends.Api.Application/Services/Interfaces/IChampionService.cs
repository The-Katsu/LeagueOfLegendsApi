using LeagueOfLegends.Api.Domain.Contracts.Responses;
using LeagueOfLegends.Api.Domain.Contracts.Responses.Champion;

namespace LeagueOfLegends.Api.Application.Services.Interfaces;

public interface IChampionService
{
    public Task<ArrayResponse<ChampionResponse>> GetAllAsync();
    public Task<ArrayResponseWithInfo<ChampionResponse>> GetPageAsync(int page);

    public Task<SingleResponse<ChampionResponseWithDetails>> GetByIdAsync(int id);
}