using LeagueOfLegends.Api.Domain.Entities.Base;
using LeagueOfLegends.Api.Infrastructure.Repositories.Interfaces;

namespace LeagueOfLegends.Api.Infrastructure;

public interface IUnitOfWork
{
    public IChampionRepository ChampionRepository { get; }
    public IAbilityRepository AbilityRepository { get; }
    public IComicRepository ComicRepository { get; }
    public IRaceRepository RaceRepository { get; }
    public IRegionRepository RegionRepository { get; }
    public IRoleRepository RoleRepository { get; }
    public ISkinRepository SkinRepository { get; }
    public IVideoRepository VideoRepository { get; }
    public IStoryRepository StoryRepository { get; }
    
    public void BeginTransaction();
    public Task AddOrUpdateAsync(Entity entity);
    public Task DeleteAsync(Entity entity);
    public Task CommitAsync(CancellationToken token = default);
}