using LeagueOfLegends.Api.Application.Mappers;
using LeagueOfLegends.Api.Application.Services.Interfaces;
using LeagueOfLegends.Api.Domain.Contracts.Responses;
using LeagueOfLegends.Api.Domain.Contracts.Responses.Video;
using LeagueOfLegends.Api.Domain.Entities;
using LeagueOfLegends.Api.Infrastructure.Repositories.Interfaces;

namespace LeagueOfLegends.Api.Application.Services.Implementations;

public class VideoService : IVideoService
{
    private readonly IVideoRepository _videoRepository;

    public VideoService(IVideoRepository videoRepository) => _videoRepository = videoRepository;

    public IQueryable<Video> GetQuery() => _videoRepository.ProvideQueryable();

    public async Task<ArrayResponse<VideoResponse>> GetAllAsync()
    {
        var videos = await _videoRepository.GetListAsync();
        return new ArrayResponse<VideoResponse>(results: videos.Select(VideoMapper.ToVideoResponse).ToList());
    }

    public async Task<SingleResponse<VideoResponseWithDetails>> GetByIdAsync(int id)
    {
        var video = await _videoRepository.GetByIdAsync(id);
        var response = video is not null ? 
            new SingleResponse<VideoResponseWithDetails>(result: video.ToVideoResponseWithDetails()) : 
            null;
        return response!;
    }
}