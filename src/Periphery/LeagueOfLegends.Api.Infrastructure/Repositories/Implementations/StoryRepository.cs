using LeagueOfLegends.Api.Domain.Entities;
using LeagueOfLegends.Api.Infrastructure.Repositories.Interfaces;
using LeagueOfLegends.Api.Persistence.NHibernate.Data;
using NHibernate.Linq;

namespace LeagueOfLegends.Api.Infrastructure.Repositories.Implementations;

public class StoryRepository : GenericRepository<Story>, IStoryRepository
{
    public StoryRepository(INHibernateDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<string>> GetTitlesAsync() => 
        await DbContext.Query<Story>()
            .Select(x => x.Title)
            .ToListAsync();
}