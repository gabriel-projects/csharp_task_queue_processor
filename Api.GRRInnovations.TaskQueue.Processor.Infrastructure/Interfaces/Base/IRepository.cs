using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TaskQueue.Processor.Infrastructure.Interfaces.Base
{
    public interface IRepository<TEntity, in TOptions> : IReadOnlyRepository<TEntity, TOptions>
    where TEntity : class
    {
        Task<TEntity> CreateAsync(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        Task SaveChangesAsync();
    }

}
