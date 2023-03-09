using LeagueOfLegends.Api.Domain.Entities;

namespace LeagueOfLegends.Api.Infrastructure.Repositories.Interfaces;

public interface IChampionRepository : IRepository<Champion>
{
    public Task<List<string>> GetChampionNamesAsync();
}