using LeagueOfLegends.Api.Domain.Entities;

namespace LeagueOfLegends.Api.Infrastructure.Repositories.Interfaces;

public interface IVideoRepository : IRepository<Video>
{
    public Task<List<string>> GetTitlesAsync();
}