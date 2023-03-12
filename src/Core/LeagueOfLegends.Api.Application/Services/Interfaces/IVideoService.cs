using LeagueOfLegends.Api.Domain.Contracts.Responses;
using LeagueOfLegends.Api.Domain.Contracts.Responses.Video;

namespace LeagueOfLegends.Api.Application.Services.Interfaces;

public interface IVideoService
{
    public Task<ArrayResponse<VideoResponse>> GetAllAsync();

    public Task<SingleResponse<VideoResponseWithDetails>> GetByIdAsync(int id);
}