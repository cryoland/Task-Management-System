using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TMS.Services
{
    public interface IRepositoryHandler<TResult>
    {
        // Create entity
        void Create(TResult entity);

        // Create entity async.
        void CreateAsync(TResult entity);

        // Returns all entities.
        IEnumerable<TResult> GetAllEntries();

        // Returns all entities async.
        Task<IEnumerable<TResult>> GetAllEntriesAsync();

        // Returns entities with specific condition.
        IEnumerable<TResult> GetAllEntries(Expression<Func<TResult, bool>> predicate);

        // Returns entities with specific condition async.
        Task<IEnumerable<TResult>> GetAllEntriesAsync(Expression<Func<TResult, bool>> predicate);

        // Returns entities with specific condition.
        TResult GetFirstEntity(Expression<Func<TResult, bool>> predicate);

        // Returns entities with specific condition async.
        Task<TResult> GetFirstEntityAsync(Expression<Func<TResult, bool>> predicate);

        // Return entity with its id.
        TResult GetEntryByID(int id);

        // Return entity with its id async.
        Task<TResult> GetEntryByIDAsync(int id);

        // Return entity with its id and specific condition.
        TResult GetEntryByID(int id, Expression<Func<TResult, bool>> predicate);

        // Return entity with its id and specific condition async.
        Task<TResult> GetEntryByIDAsync(int id, Expression<Func<TResult, bool>> predicate);

        // Update entity.
        void Update(TResult entity);

        // Update entity async.
        void UpdateAsync(TResult entity);

        // Delete entity.
        void Delete(TResult entity);

        // Delete entity async.
        void DeleteAsync(TResult entity);
    }
}