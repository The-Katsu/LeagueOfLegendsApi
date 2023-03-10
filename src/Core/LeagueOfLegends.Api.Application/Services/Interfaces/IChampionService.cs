using LeagueOfLegends.Api.Application.Contracts.Responses;
using LeagueOfLegends.Api.Application.Contracts.Responses.Champion;

namespace LeagueOfLegends.Api.Application.Services.Interfaces;

public interface IChampionService
{
    public Task<ArrayResponse<ChampionResponse>> GetAllAsync();
    public Task<ArrayResponse<ChampionResponse>> GetPageAsync(int page);
}