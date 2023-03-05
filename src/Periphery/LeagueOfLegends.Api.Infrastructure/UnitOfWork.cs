using LeagueOfLegends.Api.Domain.Entities.Base;
using LeagueOfLegends.Api.Infrastructure.Repositories.Interfaces;
using LeagueOfLegends.Api.Persistence.NHibernate.Data;

namespace LeagueOfLegends.Api.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly INHibernateDbContext _dbContext;
    private readonly Lazy<IChampionRepository> _championRepository;
    private readonly Lazy<IAbilityRepository> _abilityRepository;
    private readonly Lazy<IComicRepository> _comicRepository;
    private readonly Lazy<IRaceRepository> _raceRepository;
    private readonly Lazy<IRegionRepository> _regionRepository;
    private readonly Lazy<IRoleRepository> _roleRepository;
    private readonly Lazy<ISkinRepository> _skinRepository;
    private readonly Lazy<IVideoRepository> _videoRepository;
    private readonly Lazy<IStoryRepository> _storyRepository;

    public UnitOfWork(INHibernateDbContext dbContext, 
        Lazy<IChampionRepository> championRepository, 
        Lazy<IAbilityRepository> abilityRepository, 
        Lazy<IComicRepository> comicRepository, 
        Lazy<IRaceRepository> raceRepository, 
        Lazy<IRegionRepository> regionRepository, 
        Lazy<IRoleRepository> roleRepository, 
        Lazy<ISkinRepository> skinRepository, 
        Lazy<IStoryRepository> storyRepository, 
        Lazy<IVideoRepository> videoRepository)
    {
        _dbContext = dbContext;
        _championRepository = championRepository;
        _abilityRepository = abilityRepository;
        _comicRepository = comicRepository;
        _raceRepository = raceRepository;
        _regionRepository = regionRepository;
        _roleRepository = roleRepository;
        _skinRepository = skinRepository;
        _storyRepository = storyRepository;
        _videoRepository = videoRepository;
    }

    public IChampionRepository ChampionRepository => _championRepository.Value;
    public IAbilityRepository AbilityRepository => _abilityRepository.Value;
    public IComicRepository ComicRepository => _comicRepository.Value;
    public IRaceRepository RaceRepository => _raceRepository.Value;
    public IRegionRepository RegionRepository => _regionRepository.Value;
    public IRoleRepository RoleRepository => _roleRepository.Value;
    public ISkinRepository SkinRepository => _skinRepository.Value;
    public IVideoRepository VideoRepository => _videoRepository.Value;
    public IStoryRepository StoryRepository => _storyRepository.Value;

    public void BeginTransaction() => _dbContext.BeginTransaction();
    public async Task AddOrUpdateAsync(Entity entity) => await _dbContext.SaveAsync(entity);
    public async Task DeleteAsync(Entity entity) => await _dbContext.DeleteAsync(entity);
    public async Task CommitAsync(CancellationToken token = default)
    {
        try
        {
            await _dbContext.Commit(token);
        }
        catch
        {
            await _dbContext.RollbackAsync(token);
            throw;
        }
        finally
        {
            _dbContext.CloseTransaction();
        }
    }
}

/*
 
 * IDisposable realisation if u need it
 * (removed coz context depends to di container and dispose it at his responsibility)
 
    private bool _disposed;
    
    protected virtual void Dispose(bool disposing)
    {
        if (!disposing)
        {
            if (disposing) Dispose();
            _disposed = true;
        }
    }
    
    public void Dispose() 
    {
        Dispose(true);
        _dbContext.CloseTransaction();
        GC.SuppressFinalize(this);
    } 
    
    ~UnitOfWork() => Dispose(false);
    
 */