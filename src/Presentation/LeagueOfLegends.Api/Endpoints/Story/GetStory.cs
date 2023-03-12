using LeagueOfLegends.Api.Application.Services.Interfaces;
using LeagueOfLegends.Api.Domain.Contracts.Requests;
using LeagueOfLegends.Api.Domain.Contracts.Responses;
using LeagueOfLegends.Api.Domain.Contracts.Responses.Story;
using Microsoft.AspNetCore.Authorization;

namespace LeagueOfLegends.Api.Endpoints.Story;

[HttpGet("story/{id:int}"), AllowAnonymous]
public class GetStory : Endpoint<GetByIdRequest, SingleResponse<StoryResponseWithDetails>>
{
    private readonly IStoryService _storyService;

    public GetStory(IStoryService storyService) => _storyService = storyService;

    public override async Task HandleAsync(GetByIdRequest req, CancellationToken ct)
    {
        var response = await _storyService.GetByIdAsync(req.Id);

        if (response is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(response, ct);
    }
}