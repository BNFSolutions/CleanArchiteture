using CleanArchiteture.Domain.Repositories.Interfaces;
using CleanArchiteture.Structure.Data;
using Microsoft.EntityFrameworkCore;

namespace CleanArchiteture.Structure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly CleanArchitetureContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public BaseRepository(CleanArchitetureContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public async Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await DbSet.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await DbSet.AddAsync(entity, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            DbSet.Update(entity);
            await Context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
        {
            var entidade = await DbSet.FindAsync(new object[] { id }, cancellationToken);
            if (entidade != null)
            {
                DbSet.Remove(entidade);
                await Context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<bool> ExistsAsync(long id, CancellationToken cancellationToken = default)
        {
            return await DbSet.AnyAsync(x => EF.Property<long>(x, "Id") == id, cancellationToken);
        }
    }
}
