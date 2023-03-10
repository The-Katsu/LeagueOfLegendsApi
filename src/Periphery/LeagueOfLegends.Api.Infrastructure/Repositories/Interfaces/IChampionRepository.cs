using LeagueOfLegends.Api.Domain.Entities;

namespace LeagueOfLegends.Api.Infrastructure.Repositories.Interfaces;

public interface IChampionRepository : IRepository<Champion>
{
    public Task<List<string>> GetChampionNamesAsync();
    public Task<List<Champion>> GetChampionsPageAsync(int page);
    public Task<int> GetChampionsCountAsync();
    public Task<Champion> GetByNameAsync(string name);
}