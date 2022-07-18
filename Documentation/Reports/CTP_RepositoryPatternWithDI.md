# Implementing Repository Pattern With Dependency Injection


```
├── Controllers
│   └── ActivityController.cs
│
├── Services
│   ├── Interfaces
│   │   └── IActivityService.cs
│   │  
│   └── ActivityService.cs
│
├── Data
│   ├── Extensions
│   ├── ApplicationDatabaseContext.cs
│   └── Repositories
│       ├── Interfaces
│       │   └── IActivityRepository.cs
│       │  
│       └── ActivityRepository.cs
│ 
├── Entities
│   └── Activity.cs
│
└── Startup.cs
```

## Startup.cs
```csharp
using CTP.Data.Repositories;
using CTP.Data.Repositories.Interfaces;

public void ConfigureServices(IServiceCollection services) 
{
    services.AddScoped<IActivityService, ActivityService>();
    services.AddScoped<IActivityRepository, ActivityRepository>();
}
```

## Controllers
```csharp
public class ActivityController : Controller 
{
    private readonly IActivityService _ActivityService;

    public ActivityController(IActivityService activityService) 
    {
        _ActivityService = activityService;
    }

    public async Task<IActionResult> Index(int? courseId) 
    {
        List<Activity> results = await _ActivityService.ListAsync(courseId);

        return View(results);
    }
}
```

## Services
```csharp
 public class ActivityService : IActivityService {
    private readonly IActivityRepository _activityRepository;
    public ActivityService(IActivityRepository activeRepository) {
        _activityRepository = activeRepository;
    }

    public async Task<Activity> FindByIdAsync(int id) {
        return await _activityRepository.FindOneAsync(
            predicate: a => a.ActivityId == id,
            include: q => q.Include(a => a.ActivityType)
                .Include(a => a.Course)
                .Include(a => a.Language)
                .Include(a => a.MethodSignatures)
                    .ThenInclude(a => a.SignatureParameters.OrderBy(s => s.ParameterPosition))
                        .ThenInclude(a => a.DataType));
    }

    public async Task<IList<Activity>> ListAsync(int? courseId = null) {

        return await _activityRepository.GetAllAsync(
            predicate: courseId.HasValue ? (a => a.CourseId == courseId) : null,
            orderBy: q => q.OrderBy(a => a.ActivityId),
            include: source => source.Include(a => a.ActivityType)
            .Include(a => a.Course)
            .Include(a => a.Language)
            .Include(a => a.MethodSignatures)
                .ThenInclude(a => a.SignatureParameters.OrderBy(s => s.ParameterPosition))
                    .ThenInclude(a => a.DataType));
    }

    public async Task<IPagedList<Activity>> ListAsync(int pageIndex, int pageSize, int? courseId = null) {

        return await _activityRepository.GetListAsync(
            pageIndex: pageIndex, pageSize: pageSize,
            predicate: courseId.HasValue ? (a => a.CourseId == courseId) : null,
            orderBy: q => q.OrderBy(a => a.ActivityId),
            include: q => q.Include(a => a.ActivityType)
                .Include(a => a.Course)
                .Include(a => a.Language)
                .Include(a => a.MethodSignatures)
                    .ThenInclude(a => a.SignatureParameters.OrderBy(s => s.ParameterPosition))
                        .ThenInclude(a => a.DataType));
    }

    public async Task CreateAsync(Activity activity) {
        _activityRepository.Add(activity);
        await _activityRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(Activity activity) {
        _activityRepository.Update(activity);
        await _activityRepository.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id) {
        await _activityRepository.DeleteAsync(id);
        await _activityRepository.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id) {
        return await _activityRepository.ExistsAsync(a => a.ActivityId == id);
    }

    public async void Demo() {

        int? courseId = null;
        string term = "";

        /*--- Find One ---*/
        // Demo about Find one with projection
        var projection_item = await _activityRepository.FindOneAsync(a => new { Name = a.Title, Language = a.Language.LanguageName }, predicate: x => x.Title.Contains(term));

        // Demo about Find one with include
        Activity item = await _activityRepository.FindOneAsync(
            predicate: x => x.Title.Contains(term),
            include: source => source.Include(b => b.MethodSignatures).ThenInclude(s => s.ReturnType));

        // Demo about Find one  without include");
        item = await _activityRepository.FindOneAsync(predicate: x => x.Title.Contains(term), orderBy: source => source.OrderByDescending(b => b.ActivityId));

        
        /*--- Get All ---*/
        // Demo about Get all with projection
        var projection_itmes = await _activityRepository.GetAllAsync(a => new { Name = a.Title, Language = a.Language.LanguageName });

        // Demo about Get all with predicate
        IList<Activity> itmes = await _activityRepository.GetAllAsync(predicate: x => x.Title.Contains(term));

        // Demo about Get all with predicate, Include, ThenInclude and orderBy
        itmes = await _activityRepository.GetAllAsync(
            predicate: courseId.HasValue ? (a => a.CourseId == courseId) : null,
            orderBy: q => q.OrderBy(a => a.ActivityId),
            include: q => q.Include(a => a.ActivityType)
                .Include(a => a.Course)
                .Include(a => a.Language)
                .Include(a => a.MethodSignatures)
                    .ThenInclude(a => a.SignatureParameters.OrderBy(s => s.ParameterPosition))
                        .ThenInclude(a => a.DataType));


        /*--- Get Paged List ---*/
        // Demo about Get paged list with projection (default: pageIndex 0, pageSize 20)
        var projection_items = await _activityRepository.GetListAsync(a => new { Name = a.Title, Language = a.Language.LanguageName });

        // Demo about Get Paged List with predicate (default: pageIndex 0, pageSize 20)
        IPagedList<Activity> page = await _activityRepository.GetListAsync(predicate: x => x.Title.Contains(term));

        // Demo about Get Paged List with pageIndex, pageSize, predicate, Include, ThenInclude and orderBy
        int pageIndex = 0;
        int pageSize = 50;

        page = await _activityRepository.GetListAsync(
            pageIndex: pageIndex, pageSize: pageSize,
            predicate: courseId.HasValue ? (a => a.CourseId == courseId) : null,
            orderBy: q => q.OrderBy(a => a.ActivityId),
            include: q => q.Include(a => a.ActivityType)
                .Include(a => a.Course)
                .Include(a => a.Language)
                .Include(a => a.MethodSignatures)
                    .ThenInclude(a => a.SignatureParameters.OrderBy(s => s.ParameterPosition))
                        .ThenInclude(a => a.DataType));
    }
}
```

## ActivityRepository
```csharp
 public class ActivityRepository : GenericRepository<Activity>, IActivityRepository {

    public ActivityRepository(CTP_TESTContext context) : base(context) {

    }
}
```

## GenericRepository
```csharp
public class GenericRepository<TEntity> : IGenericRepository<TEntity>, IDisposable where TEntity : class {
    protected internal readonly CTP_TESTContext _context;
    protected internal readonly DbSet<TEntity> _dbSet;

    public GenericRepository(CTP_TESTContext context) {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = context.Set<TEntity>();
    }


    public virtual IQueryable<TEntity> GetAll(
        Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = true, bool ignoreQueryFilters = false) {

        IQueryable<TEntity> query = _dbSet;
        if (disableTracking) {
            query = query.AsNoTracking();
        }

        if (include != null) {
            query = include(query);
        }

        if (predicate != null) {
            query = query.Where(predicate);
        }

        if (ignoreQueryFilters) {
            query = query.IgnoreQueryFilters();
        }

        if (orderBy != null) {
            return orderBy(query);
        }
        else {
            return query;
        }
    }

    public IQueryable<TResult> GetAll<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = true, bool ignoreQueryFilters = false) {

        var query = GetAll(predicate, orderBy, include, disableTracking, ignoreQueryFilters);

        return query.Select(selector);
    }


    public virtual async Task<IList<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = true, bool ignoreQueryFilters = false) {

        IQueryable<TEntity> query = GetAll(predicate, orderBy, include, disableTracking, ignoreQueryFilters);

        return await query.ToListAsync();
    }

    public virtual async Task<IList<TResult>> GetAllAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = true, bool ignoreQueryFilters = false) {

        var query = GetAll(selector, predicate, orderBy, include, disableTracking, ignoreQueryFilters);

        return await query.ToListAsync();
    }

    public virtual Task<IPagedList<TEntity>> GetListAsync(
        Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        int pageIndex = 0,
        int pageSize = 20,
        bool disableTracking = true,
        CancellationToken cancellationToken = default(CancellationToken),
        bool ignoreQueryFilters = false) {

        var query = GetAll(predicate, orderBy, include, disableTracking, ignoreQueryFilters);

        return query.ToPagedListAsync(pageIndex, pageSize, 0, cancellationToken);
    }

    public virtual Task<IPagedList<TResult>> GetListAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        int pageIndex = 0,
        int pageSize = 20,
        bool disableTracking = true,
        CancellationToken cancellationToken = default(CancellationToken),
        bool ignoreQueryFilters = false) {

        var query = GetAll(predicate, orderBy, include, disableTracking, ignoreQueryFilters);

        return query.Select(selector).ToPagedListAsync(pageIndex, pageSize, 0, cancellationToken);
    }

    public virtual async Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = true, bool ignoreQueryFilters = false) {

        var query = GetAll(predicate, orderBy, include, disableTracking, ignoreQueryFilters);

        return await query.FirstOrDefaultAsync();
    }

    public virtual async Task<TResult> FindOneAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = true, bool ignoreQueryFilters = false) {

        var query = GetAll<TResult>(selector, predicate, orderBy, include, disableTracking, ignoreQueryFilters);

        return await query.FirstOrDefaultAsync();
    }

    public virtual void Add(TEntity entity) {
        _dbSet.Add(entity);
    }

    public virtual async Task DeleteAsync(object id) {
        TEntity entity = await FindAsync(id);
        _dbSet.Remove(entity);
    }

    public virtual async Task DeleteAsync(TEntity entity) {
        await Task.FromResult(_dbSet.Remove(entity));
    }


    public virtual void Update(TEntity entityToUpdate) {
        _dbSet.Attach(entityToUpdate);
        _context.Entry(entityToUpdate).State = EntityState.Modified;
    }

    public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null) {
        if (predicate == null) {
            return await _dbSet.CountAsync();
        }
        else {
            return await _dbSet.CountAsync(predicate);
        }
    }

    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate) {
        return await _dbSet.AnyAsync(predicate);
    }

    public virtual void Attach(TEntity entity) {
        var entry = _dbSet.Attach(entity);
        entry.State = EntityState.Added;
    }

    public virtual bool AddRange(params TEntity[] entities) {
        _dbSet.AddRange(entities);
        return true;
    }

    public virtual bool UpdateRange(params TEntity[] entities) {
        _dbSet.UpdateRange(entities);
        return true;
    }

    public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken)) {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task ClearAsync() {
        var allEntities = await _dbSet.ToListAsync();
        _dbSet.RemoveRange(allEntities);
    }

    protected async Task RemoveManyToManyRelationship(string joinEntityName, string ownerIdKey, string ownedIdKey, long ownerEntityId, List<long> idsToIgnore) {
        DbSet<Dictionary<string, object>> dbset = _context.Set<Dictionary<string, object>>(joinEntityName);

        var manyToManyData = await dbset
            .Where(joinPropertyBag => joinPropertyBag[ownerIdKey].Equals(ownerEntityId))
            .ToListAsync();

        var filteredManyToManyData = manyToManyData
            .Where(joinPropertyBag => !idsToIgnore.Any(idToIgnore => joinPropertyBag[ownedIdKey].Equals(idToIgnore)))
            .ToList();

        dbset.RemoveRange(filteredManyToManyData);
    }

    public virtual IQueryable<TEntity> FromSql(string sql, params object[] parameters) => _dbSet.FromSqlRaw(sql, parameters);

    public virtual ValueTask<TEntity> FindAsync(params object[] keyValues) => _dbSet.FindAsync(keyValues);

    public virtual ValueTask<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken) => _dbSet.FindAsync(keyValues, cancellationToken);

    /// <summary>
    /// Gets the async max based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    ///  /// <param name="selector"></param>
    /// <returns>decimal</returns>
    public virtual async Task<T> MaxAsync<T>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, T>> selector = null) {
        if (predicate == null)
            return await _dbSet.MaxAsync(selector);
        else
            return await _dbSet.Where(predicate).MaxAsync(selector);
    }

    /// <summary>
    /// Gets the async min based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    ///  /// <param name="selector"></param>
    /// <returns>decimal</returns>
    public virtual async Task<T> MinAsync<T>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, T>> selector = null) {
        if (predicate == null)
            return await _dbSet.MinAsync(selector);
        else
            return await _dbSet.Where(predicate).MinAsync(selector);
    }

    /// <summary>
    /// Gets the async average based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    ///  /// <param name="selector"></param>
    /// <returns>decimal</returns>
    public virtual async Task<decimal> AverageAsync(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, decimal>> selector = null) {
        if (predicate == null)
            return await _dbSet.AverageAsync(selector);
        else
            return await _dbSet.Where(predicate).AverageAsync(selector);
    }

    /// <summary>
    /// Gets the async sum based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    ///  /// <param name="selector"></param>
    /// <returns>decimal</returns>
    public virtual async Task<decimal> SumAsync(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, decimal>> selector = null) {
        if (predicate == null)
            return await _dbSet.SumAsync(selector);
        else
            return await _dbSet.Where(predicate).SumAsync(selector);
    }

    public void Dispose() {
        _context?.Dispose();
    }
}
```

## Models
```csharp
public partial class Activity
{
    private readonly IDateTimeProvider IDateTime;

    public Activity()
    {
        CodeUploads = new HashSet<CodeUpload>();
        MethodSignatures = new HashSet<MethodSignature>();
    }

    public Activity(IDateTimeProvider fakeTime) {
        CodeUploads = new HashSet<CodeUpload>();
        MethodSignatures = new HashSet<MethodSignature>();
        IDateTime = fakeTime;
    }

    [Key]
    public int ActivityId { get; set; }
    
    [Display(Name = "Title: ")]
    [DataType(System.ComponentModel.DataAnnotations.DataType.Text)]
    [StringLength(100)]
    [Required]
    public string Title { get; set; }

    [Display(Name = "Start Date: ")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
    [Required]
    public DateTime StartDate { get; set; } = DateTime.Now;

    [Display(Name = "End Date: ")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
    [Required]
    public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);
    
    [Required]
    [Display(Name = "Choose Course: ")]
    public int CourseId { get; set; }
    [Required]

    [Display(Name = "Choose Activity Type: ")]
    public int ActivityTypeId { get; set; }
    
    [Required]
    [Display(Name = "Choose Language: ")]
    public int LanguageId { get; set; }

    [Display(Name = "Activity Type: ")]
    public virtual ActivityType ActivityType { get; set; }

    [Display(Name = "Course Name: ")]
    public virtual Course Course { get; set; }

    [Display(Name = "Coding Language: ")]
    public virtual Language Language { get; set; }
    public virtual ICollection<CodeUpload> CodeUploads { get; set; }
    
    [JsonIgnore]
    public virtual ICollection<MethodSignature> MethodSignatures { get; set; }
}
```

## References
[Implementing the Repository and Unit of Work Patterns in an ASP.NET MVC Application](https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application)

[The Repository-Service Pattern with DI and ASP.NET Core](https://exceptionnotfound.net/the-repository-service-pattern-with-dependency-injection-and-asp-net-core/)

[Why injecting classes instead of interfaces is considered bad practice?](https://stackoverflow.com/questions/37829122/why-injecting-classes-instead-of-interfaces-is-considered-bad-practice)

[Implementing the Repository and Unit of Work Patterns in an ASP.NET MVC Application](https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application)

[How to add/update child entities when updating a parent entity in EF](https://stackoverflow.com/questions/27176014/how-to-add-update-child-entities-when-updating-a-parent-entity-in-ef)