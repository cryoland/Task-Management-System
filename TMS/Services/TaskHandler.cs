using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TMS.Models;
using TMS.ViewModels;

namespace TMS.Services
{
    public class TaskHandler : HandlerCore, IRepositoryHandler<QTask>
    {
        public TaskHandler(ITMSRepository repository) : base(repository) { }

        // Create entity.
        public void Create(QTask entity)
        {
            if (entity != null)
            {
                repository.QTasks.Add(entity);
                repository.Save();
            }
            else
            {
                throw new ArgumentNullException(nameof(entity));
            }
        }

        // Create entity async.
        public void CreateAsync(QTask entity)
        {
            if (entity != null)
            {
                repository.QTasks.Add(entity);
                repository.SaveAsync();
            }
            else
            {
                throw new ArgumentNullException(nameof(entity));
            }
        }

        // Returns all entities.
        public IEnumerable<QTask> GetAllEntries()
        {
            return repository.QTasks
                .Include(a => a.Assignee)
                .Include(r => r.Reporter)
                .ToList();
        }

        // Returns all entities.
        public async Task<IEnumerable<QTask>> GetAllEntriesAsync()
        {
            return await repository.QTasks
                .Include(a => a.Assignee)
                .Include(r => r.Reporter)
                .ToListAsync();
        }

        // Returns entities with specific condition.
        public IEnumerable<QTask> GetAllEntries(Expression<Func<QTask, bool>> predicate)
        {
            return repository.QTasks
                .Include(a => a.Assignee)
                .Include(r => r.Reporter)
                .Where(predicate)
                .ToList();
        }

        // Returns entities with specific condition.
        public async Task<IEnumerable<QTask>> GetAllEntriesAsync(Expression<Func<QTask, bool>> predicate)
        {
            return await repository.QTasks
                .Include(a => a.Assignee)
                .Include(r => r.Reporter)
                .Where(predicate)
                .ToListAsync();
        }

        // Returns entity with specific condition.
        public QTask GetFirstEntity(Expression<Func<QTask, bool>> predicate)
        {
            return repository.QTasks
                .Include(a => a.Assignee)
                .Include(r => r.Reporter)
                .Where(predicate)
                .FirstOrDefault();
        }

        // Returns entity with specific condition.
        public async Task<QTask> GetFirstEntityAsync(Expression<Func<QTask, bool>> predicate)
        {
            return await repository.QTasks
                .Include(a => a.Assignee)
                .Include(r => r.Reporter)
                .Where(predicate)
                .FirstOrDefaultAsync();
        }

        // Return entity with its id.
        public QTask GetEntryByID(int id)
        {
            return repository.QTasks
                .Where(t => t.Id == id)
                .FirstOrDefault();
        }

        // Return entity with its id and specific condition.
        public async Task<QTask> GetEntryByIDAsync(int id)
        {
            return await repository.QTasks
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();
        }

        // Return entity with its id and specific condition.
        public QTask GetEntryByID(int id, Expression<Func<QTask, bool>> predicate)
        {
            return repository.QTasks
                .Where(t => t.Id == id)
                .Where(predicate)
                .FirstOrDefault();
        }

        // Return entity with its id and specific condition.
        public async Task<QTask> GetEntryByIDAsync(int id, Expression<Func<QTask, bool>> predicate)
        {
            return await repository.QTasks
                .Where(t => t.Id == id)
                .Where(predicate)
                .FirstOrDefaultAsync();
        }

        // Update entity.
        public void Update(QTask entity)
        {
            if (entity != null)
            {
                var entityInDb = GetEntryByID(entity.Id);
                    entityInDb.Name = string.IsNullOrEmpty(entity.Name) ? entityInDb.Name : entity.Name;
                    entityInDb.Description = string.IsNullOrEmpty(entity.Description) ? entityInDb.Name : entity.Description;
                    entityInDb.AssigneeId = entity.AssigneeId ?? entityInDb.AssigneeId;
                    entityInDb.ReporterId = entity.ReporterId ?? entityInDb.ReporterId;
                    entityInDb.Priority = entity.Priority;
                    entityInDb.Status = entity.Status;
                repository.Save();
            }
            else
            {
                throw new ArgumentNullException(nameof(entity));
            }
        }

        // Update entity async
        public async void UpdateAsync(QTask entity)
        {
            if (entity != null)
            {
                var entityInDb = await GetEntryByIDAsync(entity.Id);
                    entityInDb.Name = string.IsNullOrEmpty(entity.Name) ? entityInDb.Name : entity.Name;
                    entityInDb.Description = string.IsNullOrEmpty(entity.Description) ? entityInDb.Name : entity.Description;
                    entityInDb.AssigneeId = entity.AssigneeId ?? entityInDb.AssigneeId;
                    entityInDb.ReporterId = entity.ReporterId ?? entityInDb.ReporterId;
                    entityInDb.Priority = entity.Priority;
                    entityInDb.Status = entity.Status;
                await repository.SaveAsync();
            }
            else
            {
                throw new ArgumentNullException(nameof(entity));
            }
        }

        // Delete entity
        public void Delete(QTask entity)
        {
            if (entity != null)
            {
                var task = repository.QTasks
                    .Where(t => t.Id == entity.Id)
                    .FirstOrDefault();
                repository.QTasks.Remove(task);
                repository.Save();
            }
            else
            {
                throw new ArgumentNullException(nameof(entity));
            }
        }

        // Delete entity async
        public async void DeleteAsync(QTask entity)
        {
            if (entity != null)
            {
                var task = await repository.QTasks
                    .Where(t => t.Id == entity.Id)
                    .FirstOrDefaultAsync();
                repository.QTasks.Remove(task);
                await repository.SaveAsync();
            }
            else
            {
                throw new ArgumentNullException(nameof(entity));
            }
        }
    }
}