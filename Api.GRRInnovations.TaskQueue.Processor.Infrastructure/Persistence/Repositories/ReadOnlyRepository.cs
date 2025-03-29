using Api.GRRInnovations.TaskQueue.Processor.Infrastructure.Persistence;
using Api.GRRInnovations.TaskQueue.Processor.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TaskQueue.Processor.Infrastructure.Persistence.Repositories
{
    public abstract class ReadOnlyRepository<TEntity, TOptions> : IReadOnlyRepository<TEntity, TOptions>
    where TEntity : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected ReadOnlyRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(TOptions options)
        {
            return await ApplyFiltering(_dbSet.AsNoTracking(), options).ToListAsync();
        }

        public virtual IQueryable<TEntity> Query(TOptions options)
        {
            return ApplyFiltering(_dbSet.AsNoTracking(), options);
        }

        protected abstract IQueryable<TEntity> ApplyFiltering(IQueryable<TEntity> query, TOptions options);
    }

}
