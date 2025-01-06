//using System.Linq.Expressions;
//using DataMigration.Domain.Common;
//using DataMigration.Infrastructure.Persistence.Context;
//using Microsoft.EntityFrameworkCore;

//namespace DataMigration.Infrastructure.Repositories.Base;

///// <summary>
///// Base repository implementation for aggregate roots.
///// </summary>
///// <typeparam name="TAggregateRoot">The type of the aggregate root.</typeparam>
//public abstract class Repository<TAggregateRoot> : IRepository<TAggregateRoot> 
//    where TAggregateRoot : AggregateRoot
//{
//    /// <summary>
//    /// The database context.
//    /// </summary>
//    protected readonly ApplicationDbContext Context;

//    /// <summary>
//    /// The DbSet for the aggregate root.
//    /// </summary>
//    protected readonly DbSet<TAggregateRoot> DbSet;

//    /// <summary>
//    /// Initializes a new instance of the repository.
//    /// </summary>
//    /// <param name="context">The database context.</param>
//    protected Repository(ApplicationDbContext context)
//    {
//        Context = context;
//        DbSet = context.Set<TAggregateRoot>();
//    }

//    /// <inheritdoc/>
//    public virtual async Task<TAggregateRoot?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
//    {
//        return await ApplyTracking(DbSet.Where(e => e.Id == id && !e.IsDeleted))
//            .FirstOrDefaultAsync(cancellationToken);
//    }

//    /// <inheritdoc/>
//    public virtual async Task<IReadOnlyList<TAggregateRoot>> GetAllAsync(CancellationToken cancellationToken = default)
//    {
//        return await ApplyTracking(DbSet.Where(e => !e.IsDeleted))
//            .ToListAsync(cancellationToken);
//    }

//    /// <inheritdoc/>
//    public virtual async Task<IReadOnlyList<TAggregateRoot>> FindAsync(
//        Expression<Func<TAggregateRoot, bool>> predicate, 
//        CancellationToken cancellationToken = default)
//    {
//        return await ApplyTracking(DbSet.Where(predicate).Where(e => !e.IsDeleted))
//            .ToListAsync(cancellationToken);
//    }

//    /// <inheritdoc/>
//    public virtual async Task AddAsync(TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default)
//    {
//        await DbSet.AddAsync(aggregateRoot, cancellationToken);
//    }

//    /// <inheritdoc/>
//    public virtual Task UpdateAsync(TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default)
//    {
//        Context.Entry(aggregateRoot).State = EntityState.Modified;
//        return Task.CompletedTask;
//    }

//    /// <inheritdoc/>
//    public virtual Task DeleteAsync(TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default)
//    {
//        // Implementing soft delete
//        aggregateRoot.IsDeleted = true;
//        Context.Entry(aggregateRoot).State = EntityState.Modified;
//        return Task.CompletedTask;
//    }

//    /// <inheritdoc/>
//    public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
//    {
//        return await Context.SaveChangesAsync(cancellationToken);
//    }

//    /// <summary>
//    /// Applies tracking settings to a query.
//    /// </summary>
//    /// <param name="query">The query to apply tracking to.</param>
//    /// <returns>The query with tracking settings applied.</returns>
//    protected virtual IQueryable<TAggregateRoot> ApplyTracking(IQueryable<TAggregateRoot> query)
//    {
//        // By default, track entities for aggregate roots
//        return query;
//    }
//} 