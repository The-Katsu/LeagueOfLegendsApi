using LeagueOfLegends.Api.Domain.Contracts.Responses;
using LeagueOfLegends.Api.Domain.Contracts.Responses.Story;

namespace LeagueOfLegends.Api.Application.Services.Interfaces;

public interface IStoryService
{
    public Task<ArrayResponse<StoryResponse>> GetAllAsync();

    public Task<SingleResponse<StoryResponseWithDetails>> GetByIdAsync(int id);
}