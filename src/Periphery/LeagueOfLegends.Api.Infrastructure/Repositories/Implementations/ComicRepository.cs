using LeagueOfLegends.Api.Domain.Entities;
using LeagueOfLegends.Api.Infrastructure.Repositories.Interfaces;
using LeagueOfLegends.Api.Persistence.NHibernate.Data;
using NHibernate.Linq;

namespace LeagueOfLegends.Api.Infrastructure.Repositories.Implementations;

public class ComicRepository : GenericRepository<Comic>, IComicRepository
{
    public ComicRepository(INHibernateDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<string>> GetTitlesAsync() =>
        await DbContext.Query<Comic>()
            .Select(x => x.Title)
            .ToListAsync();
}