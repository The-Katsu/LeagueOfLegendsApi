using LeagueOfLegends.Api.Application.Services.Interfaces;
using LeagueOfLegends.Api.Domain.Contracts.Responses;
using LeagueOfLegends.Api.Domain.Contracts.Responses.Story;
using Microsoft.AspNetCore.Authorization;

namespace LeagueOfLegends.Api.Endpoints.Story;

[HttpGet("story"), AllowAnonymous]
public class GetStories : EndpointWithoutRequest<ArrayResponse<StoryResponse>>
{
    private readonly IStoryService _storyService;

    public GetStories(IStoryService storyService) => _storyService = storyService;

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await _storyService.GetAllAsync();
        await SendOkAsync(response, ct);
    }
}