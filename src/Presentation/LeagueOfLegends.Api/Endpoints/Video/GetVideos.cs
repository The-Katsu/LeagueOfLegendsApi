using LeagueOfLegends.Api.Application.Services.Interfaces;
using LeagueOfLegends.Api.Domain.Contracts.Responses;
using LeagueOfLegends.Api.Domain.Contracts.Responses.Video;
using Microsoft.AspNetCore.Authorization;

namespace LeagueOfLegends.Api.Endpoints.Video;

[HttpGet("video"), AllowAnonymous]
public class GetVideos : EndpointWithoutRequest<ArrayResponse<VideoResponse>>
{
    private readonly IVideoService _videoService;

    public GetVideos(IVideoService videoService) => _videoService = videoService;

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await _videoService.GetAllAsync();
        await SendOkAsync(response, ct);
    }
}