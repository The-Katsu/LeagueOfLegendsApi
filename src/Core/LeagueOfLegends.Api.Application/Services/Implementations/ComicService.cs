using LeagueOfLegends.Api.Application.Mappers;
using LeagueOfLegends.Api.Application.Services.Interfaces;
using LeagueOfLegends.Api.Domain.Contracts.Responses;
using LeagueOfLegends.Api.Domain.Contracts.Responses.Comic;
using LeagueOfLegends.Api.Infrastructure.Repositories.Interfaces;

namespace LeagueOfLegends.Api.Application.Services.Implementations;

public class ComicService : IComicService
{
    private readonly IComicRepository _comicRepository;

    public ComicService(IComicRepository comicRepository) => _comicRepository = comicRepository;

    public async Task<ArrayResponse<ComicResponse>> GetAllAsync()
    {
        var comics = await _comicRepository.GetListAsync();
        return new ArrayResponse<ComicResponse>(results: comics.Select(ComicMapper.ToComicResponse).ToList());
    }

    public async Task<SingleResponse<ComicResponseWithDetails>> GetByIdAsync(int id)
    {
        var story = await _comicRepository.GetByIdAsync(id);
        var response = story is not null ? 
            new SingleResponse<ComicResponseWithDetails>(result: story.ToComicResponseWithDetails()) : 
            null;
        return response!;
    }
}