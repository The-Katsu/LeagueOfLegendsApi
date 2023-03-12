using LeagueOfLegends.Api.Domain.Entities;

namespace LeagueOfLegends.Api.Infrastructure.Repositories.Interfaces;

public interface IRaceRepository : IRepository<Race>
{
    public Task<Race> GetByNameAsync(string name);
}