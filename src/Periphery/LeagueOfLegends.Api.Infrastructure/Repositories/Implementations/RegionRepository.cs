using LeagueOfLegends.Api.Domain.Entities;
using LeagueOfLegends.Api.Infrastructure.Repositories.Interfaces;
using LeagueOfLegends.Api.Persistence.NHibernate.Data;
using NHibernate.Linq;

namespace LeagueOfLegends.Api.Infrastructure.Repositories.Implementations;

public class RegionRepository : GenericRepository<Region>, IRegionRepository
{
    public RegionRepository(INHibernateDbContext dbContext) : base(dbContext) { }

    public async Task<List<string>> GetNamesAsync() => 
        await DbContext.Query<Region>().Select(x => x.Name).ToListAsync();
}