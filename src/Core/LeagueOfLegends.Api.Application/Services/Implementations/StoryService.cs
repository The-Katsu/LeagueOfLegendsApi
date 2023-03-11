using LeagueOfLegends.Api.Application.Mappers;
using LeagueOfLegends.Api.Application.Services.Interfaces;
using LeagueOfLegends.Api.Domain.Contracts.Responses;
using LeagueOfLegends.Api.Domain.Contracts.Responses.Story;
using LeagueOfLegends.Api.Infrastructure.Repositories.Interfaces;

namespace LeagueOfLegends.Api.Application.Services.Implementations;

public class StoryService : IStoryService
{
    private readonly IStoryRepository _storyRepository;

    public StoryService(IStoryRepository storyRepository) => _storyRepository = storyRepository;

    public async Task<ArrayResponse<StoryResponse>> GetAllAsync()
    {
        var stories = await _storyRepository.GetListAsync();
        return new ArrayResponse<StoryResponse>(results: stories.Select(StoryMapper.ToStoryResponse).ToList());
    }

    public async Task<SingleResponse<StoryResponseWithDetails>> GetByIdAsync(int id)
    {
        var story = await _storyRepository.GetByIdAsync(id);
        var response = story is not null ? 
            new SingleResponse<StoryResponseWithDetails>(result: story.ToStoryResponseWithDetails()) : 
            null;
        return response!;
    }
}