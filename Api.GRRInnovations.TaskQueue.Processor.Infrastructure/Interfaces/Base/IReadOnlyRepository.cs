﻿namespace Api.GRRInnovations.TaskQueue.Processor.Infrastructure.Interfaces.Base
{
    public interface IReadOnlyRepository<TEntity, in TOptions> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync(TOptions options);
        IQueryable<TEntity> Query(TOptions options);
    }
}
