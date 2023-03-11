using LeagueOfLegends.Api.Domain.Contracts.Responses;
using LeagueOfLegends.Api.Domain.Contracts.Responses.Comic;

namespace LeagueOfLegends.Api.Application.Services.Interfaces;

public interface IComicService
{
    public Task<ArrayResponse<ComicResponse>> GetAllAsync();

    public Task<SingleResponse<ComicResponseWithDetails>> GetByIdAsync(int id);
}