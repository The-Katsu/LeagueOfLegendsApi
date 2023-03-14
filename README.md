# League Of Legends Api  
  ---
Check out the documentation to get started - http://www.lolapidocumentation.somee.com/
  
Explore League Of Legends Universe with:  
GraphQl playground - https://leagueapi-001-site1.etempurl.com/graphql/  
Swagger UI - https://leagueapi-001-site1.etempurl.com/swagger/index.html  


---  

- Navigation
    - [DDD](#domain-driven-design)
    - [Configure NHibernate](#configure-nhibernate)
    - [Unit of Work with lazy repositories  ](#unit-of-work-with-lazy-repositories)
    - [Fast Endpoints](#fast-endpoints)
    - [Hot Chocolate](#hot-chocolate)

---  

## Domain Driven Design  

### Base DDD project structure

```powershell
|-src
    |--Core
    |   |--Application
    |   |--Domain
    |
    |--Periphery
    |   |--Infrastructure
    |   |--Persistence
    |
    |--Presentation
        |--API
```  

How to use layers (imho):
 
Use Domain to store common things for all project like Entities, Contracts, Exceptions etc.  
Use Persistence to configure with external services like ORM, Message brokers, Task schedulers etc.  
Use Infrastructure for base external services logic implementation.  
Use Application to provide business logic.  
Use Presentation to display results of your services.  


## Configure NHibernate  
### NHibernate sql migration to local folder  
``` cs
public static class NHibernateMigrationsManager
{
    private static readonly string Path = "YOUR_PATH_HERE";

    public static bool InitCreated() => new DirectoryInfo(Path).GetFiles().Any(d => d.Extension == ".sql");
    
    public static Action<string> InitMigration => x =>
    {
        using var stream = new FileStream($"{Path}/init.sql", FileMode.Append, FileAccess.Write);
        using var writer = new StreamWriter(stream);
        writer.Write($"{x}\n;");
    };

    public static Action<string> UpdateMigration => x =>
    {
        var now = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss");
        using var stream = new FileStream($"{Path}/migration{now}.sql", FileMode.Append, FileAccess.Write);
        using var writer = new StreamWriter(stream);
        writer.Write($"{x}\n;");
    };
}
```

### NHibernate context  
```cs
public interface INHibernateDbContext
{
    public void BeginTransaction();
    public Task CommitAsync(CancellationToken token = default);
    public Task RollbackAsync(CancellationToken token = default);
    public void CloseTransaction();
    public Task SaveOrUpdateAsync(Entity entity, CancellationToken token = default);
    public Task DeleteAsync(Entity entity, CancellationToken token = default);

    public Task<T> GetByIdAsync<T>(int id) where T : Entity;
    public IQueryable<T> Query<T>() where T : Entity;
    public IQueryOver<T, T> QueryOver<T>() where T : Entity;
}

public class NHibernateDbContext : INHibernateDbContext
{
    private readonly ISession _session;
    private ITransaction? _transaction;

    public NHibernateDbContext(ISession session) => _session = session;

    public IQueryable<T> Query<T>() where T : Entity => _session.Query<T>();
    
    public IQueryOver<T, T> QueryOver<T>() where T : Entity => _session.QueryOver<T>(); 
    
    public async Task<T> GetByIdAsync<T>(int id) where T : Entity => await _session.GetAsync<T>(id);

    public void BeginTransaction() => 
        _transaction = _session.BeginTransaction();

    public async Task CommitAsync(CancellationToken token) => 
        await _transaction?.CommitAsync(token)!;

    public async Task RollbackAsync(CancellationToken token = default) => 
        await _transaction?.RollbackAsync(token)!;

    public async Task SaveOrUpdateAsync(Entity entity, CancellationToken token = default) => 
        await _session.SaveOrUpdateAsync(entity, token);

    public async Task DeleteAsync(Entity entity, CancellationToken token = default) => 
        await _session.DeleteAsync(entity, token);

    public void CloseTransaction()
    {
        if (_transaction is null) return;
        _session.Clear();
        _transaction.Dispose();
        _transaction = null;
    }
}
```

### NHibernate DI extension

```cs
public static IServiceCollection AddNHibernate(this IServiceCollection services, string connectionString)
{
    var mapper = new ModelMapper();
    mapper.AddMappings(typeof(NHibernateExtension).Assembly.ExportedTypes);
    var hbmMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

    var configuration = new Configuration();
    configuration.DataBaseIntegration(p =>
    {
        p.Dialect<PostgreSQLDialect>();
        p.ConnectionString = connectionString;
        p.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
        p.SchemaAction = SchemaAutoAction.Validate;
#if DEBUG
        p.LogFormattedSql = true;
        p.LogSqlInConsole = true;
#endif
    });
    configuration.AddMapping(hbmMapping);
    
#if DEBUG
    if(!NHibernateMigrationsManager.InitCreated()) 
        new SchemaExport(configuration)
            .Create(NHibernateMigrationsManager.InitMigration, true);
    else
        new SchemaUpdate(configuration)
            .Execute(NHibernateMigrationsManager.UpdateMigration, true);
#endif
    
    var sessionFactory = configuration.BuildSessionFactory();

    services.AddSingleton(sessionFactory);
    services.AddScoped(_ => sessionFactory.OpenSession());
    services.AddScoped<INHibernateDbContext, NHibernateDbContext>();
    
    return services;
}
```

## Unit of Work with lazy repositories  

#### Unit of work

```cs
public interface IUnitOfWork
{
    public IRepository1 Repository1 { get; }
    public IRepository2 Repository2 { get; }
    public IRepository3 Repository3 { get; }

    public void BeginTransaction();
    public Task AddOrUpdateAsync(Entity entity);
    public Task DeleteAsync(Entity entity);
    public Task CommitAsync(CancellationToken token = default);
}

public class UnitOfWork : IUnitOfWork
{
    private readonly INHibernateDbContext _dbContext;
    private readonly Lazy<IRepository1> _repository1;
    private readonly Lazy<IRepository2> _repository2;
    private readonly Lazy<IRepository3> _repository3;

    public UnitOfWork(INHibernateDbContext dbContext, 
        Lazy<IRepository1> repository1, 
        Lazy<IRepository2> repository2, 
        Lazy<IRepository3> repository3)
    {
        _dbContext = dbContext;
        _repository1 = repository1;
        _repository2 = repository2;
        _repository3 = repository3;
    }

    public IRepository1 Repository1 => _repository1.Value;
    public IRepository2 Repository2 => _repository2.Value;
    public IRepository3 Repository3 => _repository3.Value;

    public void BeginTransaction() => _dbContext.BeginTransaction();
    public async Task AddOrUpdateAsync(Entity entity) => await _dbContext.SaveOrUpdateAsync(entity);
    public async Task DeleteAsync(Entity entity) => await _dbContext.DeleteAsync(entity);
    public async Task CommitAsync(CancellationToken token = default)
    {
        try
        {
            await _dbContext.CommitAsync(token);
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
```

### Allow lazy repositories extension  
```cs
public static IServiceCollection AllowLazy<T>(this IServiceCollection services)
{
    var service = services.Last();
    var descriptor = new ServiceDescriptor(
        typeof(Lazy<T>),
        provider => new Lazy<T>(provider.GetService<T>()!),
        service.Lifetime
    );
    services.Add(descriptor);
    return services;
}
```

### Register services  
```cs
public static IServiceCollection AddInfrastructure(this IServiceCollection services)
{
    services.AddScoped<IRepository1, Repository1>().AllowLazy<IRepository1>();
    services.AddScoped<IRepository2, Repository2>().AllowLazy<IRepository2>();
    services.AddScoped<IRepository3, Repository3>().AllowLazy<IRepository3>();
    
    services.AddScoped<IUnitOfWork, UnitOfWork>();
    
    return services;
}
```  

## Fast Endpoints  

### RERP pattern example (request-endpoint-response pattern)

### 1. Add packages (FastEndpoints & FastEndpoints.Swagger)   
### 2. Register services
```cs 
builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDoc();

app.UseFastEndpoints(c => c.Endpoints.RoutePrefix = "api");
app.UseSwaggerGen();
```   
### 3. Endpoint example  
* Endoint with request
```cs
[HttpGet("champion/{id:int}"), AllowAnonymous]
public class GetChampion : Endpoint<GetByIdRequest, SingleResponse<ChampionResponseWithDetails>>
{
    private readonly IChampionService _championService;

    public GetChampion(IChampionService championService) => _championService = championService;

    public override async Task HandleAsync(GetByIdRequest req, CancellationToken ct)
    {
        var response = await _championService.GetByIdAsync(req.Id);

        if (response is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(response, ct);
    }
}
```  
* Endpoint without request  
```cs
[HttpGet("champion"), AllowAnonymous]
public class GetChampions : EndpointWithoutRequest<ArrayResponse<ChampionResponse>>
{
    private readonly IChampionService _championService;

    public GetChampions(IChampionService championService) => _championService = championService;

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await _championService.GetAllAsync();
        await SendOkAsync(response, ct);
    }
}
```  

## Hot Chocolate  
  
### GraphQL C# server example

### 1. Add packages (HotChocolate.AspNetCore & HotChocolate.Data)   
### 2. Register services
```cs 
builder.Services
        .AddGraphQLServer()
        .AddQueryType(q => q.Name("Query"))
        .AddType<ChampionQueries>()
        .AddType<RegionQueries>()
        .AddType<ComicQueries>()
        .AddType<StoryQueries>()
        .AddType<VideoQueries>()
        .AddSorting()
        .AddFiltering()
        .AddProjections();

app.MapGraphQL();
```   
### 3. Query example  
* Single
```cs
[UseFirstOrDefault]
[UseProjection]
[UseFiltering]
public IQueryable<Champion> Champion([Service] IChampionService championService) =>
    championService.GetQuery();
```  
* Array with pagination
```cs
[UseOffsetPaging(DefaultPageSize = 20, IncludeTotalCount = true)]
[UseProjection]
[UseFiltering]
[UseSorting]
public IQueryable<Champion> Champions([Service] IChampionService championService) =>
    championService.GetQuery();
```
