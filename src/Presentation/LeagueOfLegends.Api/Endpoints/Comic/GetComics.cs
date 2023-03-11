using LeagueOfLegends.Api.Application.Services.Interfaces;
using LeagueOfLegends.Api.Domain.Contracts.Responses;
using LeagueOfLegends.Api.Domain.Contracts.Responses.Comic;
using LeagueOfLegends.Api.Infrastructure.Repositories.Implementations;
using LeagueOfLegends.Api.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace LeagueOfLegends.Api.Endpoints.Comic;

[HttpGet("comic"), AllowAnonymous]
public class GetComics : EndpointWithoutRequest<ArrayResponse<ComicResponse>>
{
    private readonly IComicService _comicService;

    public GetComics(IComicService comicService) => _comicService = comicService;

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await _comicService.GetAllAsync();
        await SendOkAsync(response, ct);
    }
}