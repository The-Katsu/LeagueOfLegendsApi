using LeagueOfLegends.Api.Application.Services.Interfaces;
using LeagueOfLegends.Api.Domain.Contracts.Requests;
using LeagueOfLegends.Api.Domain.Contracts.Responses;
using LeagueOfLegends.Api.Domain.Contracts.Responses.Video;
using Microsoft.AspNetCore.Authorization;

namespace LeagueOfLegends.Api.Endpoints.Video;

[HttpGet("video/{id:int}"), AllowAnonymous]
public class GetVideo : Endpoint<GetByIdRequest, SingleResponse<VideoResponseWithDetails>>
{
    private readonly IVideoService _videoService;

    public GetVideo(IVideoService videoService) => _videoService = videoService;

    public override async Task HandleAsync(GetByIdRequest req, CancellationToken ct)
    {
        var response = await _videoService.GetByIdAsync(req.Id);

        if (response is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(response, ct);
    }
}