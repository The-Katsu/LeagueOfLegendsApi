using LeagueOfLegends.Api.Domain.Entities;
using LeagueOfLegends.Api.Persistence.NHibernate.Data;

namespace LeagueOfLegends.Api.Application.GraphQl;

public class QueryProvider : IQueryProvider
{
    private readonly INHibernateDbContext _dbContext;
    
    public QueryProvider(INHibernateDbContext dbContext) => _dbContext = dbContext;
    
    public IQueryable<Champion> Champions => _dbContext.Query<Champion>();
    public IQueryable<Video> Videos => _dbContext.Query<Video>();
    public IQueryable<Story> Stories => _dbContext.Query<Story>();
    public IQueryable<Region> Regions => _dbContext.Query<Region>();
    public IQueryable<Comic> Comics => _dbContext.Query<Comic>();
}