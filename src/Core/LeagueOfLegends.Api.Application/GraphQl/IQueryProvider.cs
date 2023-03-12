using LeagueOfLegends.Api.Domain.Entities;

namespace LeagueOfLegends.Api.Application.GraphQl;

public interface IQueryProvider
{
    public IQueryable<Champion> Champions { get; }
    public IQueryable<Video> Videos { get; }
    public IQueryable<Story> Stories { get; }
    public IQueryable<Region> Regions { get; }
    public IQueryable<Comic> Comics { get; }
}