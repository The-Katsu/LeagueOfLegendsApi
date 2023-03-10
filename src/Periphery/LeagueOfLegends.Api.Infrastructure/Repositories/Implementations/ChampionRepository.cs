using LeagueOfLegends.Api.Domain.Entities;
using LeagueOfLegends.Api.Infrastructure.Repositories.Interfaces;
using LeagueOfLegends.Api.Persistence.NHibernate.Data;
using NHibernate.Linq;

namespace LeagueOfLegends.Api.Infrastructure.Repositories.Implementations;

public class ChampionRepository : GenericRepository<Champion>, IChampionRepository
{
    public ChampionRepository(INHibernateDbContext dbContext) : base(dbContext) { }
    
    public async Task<List<string>> GetChampionNamesAsync() => 
        await DbContext.Query<Champion>()
            .Select(x => x.Name)
            .ToListAsync();

    public async Task<List<Champion>> GetChampionsPageAsync(int page) =>
        await DbContext.Query<Champion>()
            .OrderBy(x => x.Name)
            .Skip(20 * page)
            .Take(20)
            .ToListAsync();

    public async Task<int> GetChampionsCountAsync() =>
        await DbContext.Query<Champion>()
            .CountAsync();

    public async Task<Champion> GetByNameAsync(string name) => 
        await DbContext.Query<Champion>()
            .FirstOrDefaultAsync(x => x.Name == name);

    public override async Task<IList<Champion>> GetListAsync() => 
        await DbContext.Query<Champion>()
            .OrderBy(x => x.Name)
            .ToListAsync();
}