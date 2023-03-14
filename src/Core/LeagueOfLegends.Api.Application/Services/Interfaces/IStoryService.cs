using LeagueOfLegends.Api.Domain.Contracts.Responses;
using LeagueOfLegends.Api.Domain.Contracts.Responses.Story;
using LeagueOfLegends.Api.Domain.Entities;

namespace LeagueOfLegends.Api.Application.Services.Interfaces;

public interface IStoryService
{
    public IQueryable<Story> GetQuery();
    public Task<ArrayResponse<StoryResponse>> GetAllAsync();

    public Task<SingleResponse<StoryResponseWithDetails>> GetByIdAsync(int id);
}