using LeagueOfLegends.Api.Domain.Contracts.Responses;
using LeagueOfLegends.Api.Domain.Contracts.Responses.Video;
using LeagueOfLegends.Api.Domain.Entities;

namespace LeagueOfLegends.Api.Application.Services.Interfaces;

public interface IVideoService
{
    public IQueryable<Video> GetQuery();
    public Task<ArrayResponse<VideoResponse>> GetAllAsync();

    public Task<SingleResponse<VideoResponseWithDetails>> GetByIdAsync(int id);
}