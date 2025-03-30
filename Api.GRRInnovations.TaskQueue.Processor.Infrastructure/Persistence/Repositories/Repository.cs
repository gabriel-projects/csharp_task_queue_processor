using Api.GRRInnovations.TaskQueue.Processor.Infrastructure.Interfaces.Base;
using Api.GRRInnovations.TaskQueue.Processor.Infrastructure.Persistence;
using Api.GRRInnovations.TaskQueue.Processor.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TaskQueue.Processor.Infrastructure.Persistence.Repositories
{
    public abstract class Repository<TEntity, TOptions> : ReadOnlyRepository<TEntity, TOptions>, IRepository<TEntity, TOptions>
    where TEntity : class
    {
        protected Repository(ApplicationDbContext context) : base(context) { }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
