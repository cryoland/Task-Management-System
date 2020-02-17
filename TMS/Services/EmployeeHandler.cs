using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TMS.Models;

namespace TMS.Services
{
    public class EmployeeHandler : HandlerCore, IRepositoryHandler<Employees>
    {
        public EmployeeHandler(ITMSRepository repository) : base(repository) { }

        public void Create(Employees entity)
        {
            if (entity != null)
            {
                repository.Employees.Add(entity);
                repository.Save();
            }
            else
            {
                throw new ArgumentNullException(nameof(entity));
            }
        }

        public void CreateAsync(Employees entity)
        {
            if (entity != null)
            {
                repository.Employees.Add(entity);
                repository.SaveAsync();
            }
            else
            {
                throw new ArgumentNullException(nameof(entity));
            }
        }

        public void Delete(Employees entity)
        {
            if (entity != null)
            {
                var empl = repository.Employees
                    .Where(t => t.Id == entity.Id)
                    .FirstOrDefault();
                repository.Employees.Remove(empl);
                repository.Save();
            }
            else
            {
                throw new ArgumentNullException(nameof(entity));
            }
        }

        public async void DeleteAsync(Employees entity)
        {
            if (entity != null)
            {
                var empl = await repository.Employees
                    .Where(t => t.Id == entity.Id)
                    .FirstOrDefaultAsync();
                repository.Employees.Remove(empl);
                await repository.SaveAsync();
            }
            else
            {
                throw new ArgumentNullException(nameof(entity));
            }
        }

        public IEnumerable<Employees> GetAllEntries()
        {
            return repository.Employees.Include(r => r.Role).ToList();
        }

        public IEnumerable<Employees> GetAllEntries(Expression<Func<Employees, bool>> predicate)
        {
            return repository.Employees.Where(predicate).Include(r => r.Role).ToList();
        }

        public async Task<IEnumerable<Employees>> GetAllEntriesAsync()
        {
            return await repository.Employees.Include(r => r.Role).ToListAsync();
        }

        public async Task<IEnumerable<Employees>> GetAllEntriesAsync(Expression<Func<Employees, bool>> predicate)
        {
            return await repository.Employees.Where(predicate).Include(r => r.Role).ToListAsync();
        }

        public Employees GetFirstEntity(Expression<Func<Employees, bool>> predicate)
        {
            return repository.Employees
                .Include(r => r.Role)
                .Where(predicate)
                .FirstOrDefault();
        }

        public async Task<Employees> GetFirstEntityAsync(Expression<Func<Employees, bool>> predicate)
        {
            return await repository.Employees
                .Include(r => r.Role)
                .Where(predicate)
                .FirstOrDefaultAsync();
        }

        public Employees GetEntryByID(int id)
        {
            return repository.Employees.Where(t => t.Id == id).Include(r => r.Role).FirstOrDefault();
        }

        public Employees GetEntryByID(int id, Expression<Func<Employees, bool>> predicate)
        {
            return repository.Employees.Where(t => t.Id == id).Where(predicate).Include(r => r.Role).FirstOrDefault();
        }

        public async Task<Employees> GetEntryByIDAsync(int id)
        {
            return await repository.Employees.Where(t => t.Id == id).Include(r => r.Role).FirstOrDefaultAsync();
        }

        public async Task<Employees> GetEntryByIDAsync(int id, Expression<Func<Employees, bool>> predicate)
        {
            return await repository.Employees.Where(t => t.Id == id).Where(predicate).Include(r => r.Role).FirstOrDefaultAsync();
        }

        public void Update(Employees entity)
        {
            if (entity != null)
            {
                var entityInDb = GetEntryByID(entity.Id);
                entityInDb.ShortName = string.IsNullOrEmpty(entity.ShortName) ? entityInDb.ShortName : entity.ShortName;
                entityInDb.FullName = string.IsNullOrEmpty(entity.FullName) ? entityInDb.FullName : entity.FullName;
                entityInDb.Email = string.IsNullOrEmpty(entity.Email) ? entityInDb.Email : entity.Email;
                entityInDb.Password = string.IsNullOrEmpty(entity.Password) ? entityInDb.Password : entity.Password;
                entityInDb.Role = entity.Role ?? repository.Roles.FirstOrDefault(r => r.Id == entityInDb.Id);
                repository.Save();
            }
            else
            {
                throw new ArgumentNullException(nameof(entity));
            }
        }

        public async void UpdateAsync(Employees entity)
        {
            if (entity != null)
            {
                var entityInDb = await GetEntryByIDAsync(entity.Id);
                entityInDb.ShortName = string.IsNullOrEmpty(entity.ShortName) ? entityInDb.ShortName : entity.ShortName;
                entityInDb.FullName = string.IsNullOrEmpty(entity.FullName) ? entityInDb.FullName : entity.FullName;
                entityInDb.Email = string.IsNullOrEmpty(entity.Email) ? entityInDb.Email : entity.Email;
                entityInDb.Password = string.IsNullOrEmpty(entity.Password) ? entityInDb.Password : entity.Password;
                entityInDb.Role = entity.Role ?? await repository.Roles.FirstOrDefaultAsync(r => r.Id == entityInDb.Id);
                await repository.SaveAsync();
            }
            else
            {
                throw new ArgumentNullException(nameof(entity));
            }
        }
    }
}