using LeagueOfLegends.Api.Domain.Entities;

namespace LeagueOfLegends.Api.Infrastructure.Repositories.Interfaces;

public interface IRoleRepository : IRepository<Role>
{
    public Task<Role> GetByNameAsync(string name);
}