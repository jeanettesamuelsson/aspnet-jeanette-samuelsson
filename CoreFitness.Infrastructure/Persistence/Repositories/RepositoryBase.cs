using CoreFitness.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoreFitness.Infrastructure.Persistence.Repositories;

public abstract class RepositoryBase<TDomainModel, TId, TEntity, TDbContext>(TDbContext context) : IRepositoryBase<TDomainModel, TId> where TEntity : class where TDbContext : DbContext
{
    protected readonly TDbContext _context = context;

    protected DbSet<TEntity> Set => _context.Set<TEntity>();

    protected abstract TId GetId(TDomainModel model);

    protected abstract TEntity ToEntity(TDomainModel model);

    protected abstract TDomainModel ToDomainModel(TEntity entity);

    protected abstract void ApplyPropertyUpdates(TEntity entity, TDomainModel model);

    public virtual async Task AddAsync(TDomainModel model, CancellationToken cancellationToken = default)
    {
        try
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var entity = ToEntity(model);

            await Set.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

        }
        catch
        {

            throw;
        }

    }


    public virtual async Task<bool> UpdateAsync(TDomainModel model, CancellationToken cancellationToken = default)
    {
        try
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var id = GetId(model);
            var entity = await Set.FindAsync([id], cancellationToken);

            if (entity == null)
                return false;

            //apply updates to entity, save changes and return true
            ApplyPropertyUpdates(entity, model);
            await _context.SaveChangesAsync(cancellationToken);

            return true;

        }
        catch
        {

            throw;
        }

    }

    public virtual async Task<bool> RemoveAsync(TDomainModel model, CancellationToken cancellationToken = default)
    {
        try
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var id = GetId(model);
            var entity = await Set.FindAsync([id], cancellationToken);

            if (entity == null)
                return false;

            //remove entity, save changes and return true
            Set.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;

        }
        catch
        {

            throw;
        }

    }

    public virtual async Task<TDomainModel?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await Set.FindAsync([id], cancellationToken);
            return entity is null ? default : ToDomainModel(entity);
        }

        catch
        {

            throw;
        }

    }
    public virtual async Task<IReadOnlyList<TDomainModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var entities = await Set.AsNoTracking().ToListAsync(cancellationToken);
            return [.. entities.Select(ToDomainModel)];
        }

        catch
        {

            throw;
        }

    }
}
