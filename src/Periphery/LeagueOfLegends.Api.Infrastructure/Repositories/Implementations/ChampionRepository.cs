using LeagueOfLegends.Api.Domain.Entities;
using LeagueOfLegends.Api.Infrastructure.Repositories.Interfaces;
using LeagueOfLegends.Api.Persistence.NHibernate.Data;
using NHibernate.Linq;

namespace LeagueOfLegends.Api.Infrastructure.Repositories.Implementations;

public class ChampionRepository : GenericRepository<Champion>, IChampionRepository
{
    public ChampionRepository(INHibernateDbContext dbContext) : base(dbContext) { }
    
    public async Task<List<string>> GetChampionNamesAsync() => 
        await DbContext.Query<Champion>().Select(x => x.Name).ToListAsync();

}