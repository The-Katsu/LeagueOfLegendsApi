using LeagueOfLegends.Api.Application.Services.Interfaces;
using LeagueOfLegends.Api.Domain.Contracts.Requests;
using LeagueOfLegends.Api.Domain.Contracts.Responses;
using LeagueOfLegends.Api.Domain.Contracts.Responses.Comic;
using Microsoft.AspNetCore.Authorization;

namespace LeagueOfLegends.Api.Endpoints.Comic;

[HttpGet("comic/{id:int}"), AllowAnonymous]
public class GetComic : Endpoint<GetByIdRequest, SingleResponse<ComicResponseWithDetails>>
{
    private readonly IComicService _comicService;

    public GetComic(IComicService comicService) => _comicService = comicService;

    public override async Task HandleAsync(GetByIdRequest req, CancellationToken ct)
    {
        var response = await _comicService.GetByIdAsync(req.Id);

        if (response is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(response, ct);
    }
}