using LeagueOfLegends.Api.Domain.Contracts.Responses;
using LeagueOfLegends.Api.Domain.Contracts.Responses.Comic;
using LeagueOfLegends.Api.Domain.Entities;

namespace LeagueOfLegends.Api.Application.Services.Interfaces;

public interface IComicService
{
    public IQueryable<Comic> GetQuery();
    public Task<ArrayResponse<ComicResponse>> GetAllAsync();

    public Task<SingleResponse<ComicResponseWithDetails>> GetByIdAsync(int id);
}