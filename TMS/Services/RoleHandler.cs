using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TMS.Models;

namespace TMS.Services
{
    public class RoleHandler : HandlerCore, IRepositoryHandler<Role>
    {
        public RoleHandler(ITMSRepository repository) : base(repository) { }
        public void Create(Role entity)
        {
            throw new NotImplementedException();
        }

        public async void CreateAsync(Role entity)
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }

        public void Delete(Role entity)
        {
            throw new NotImplementedException();
        }

        public async void DeleteAsync(Role entity)
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetAllEntries()
        {
            return repository.Roles.Include(r => r.Users).ToList();
        }

        public IEnumerable<Role> GetAllEntries(Expression<Func<Role, bool>> predicate)
        {
            return repository.Roles.Include(r => r.Users).Where(predicate).ToList();
        }

        public async Task<IEnumerable<Role>> GetAllEntriesAsync()
        {
            return await repository.Roles.Include(r => r.Users).ToListAsync();
        }

        public async Task<IEnumerable<Role>> GetAllEntriesAsync(Expression<Func<Role, bool>> predicate)
        {
            return await repository.Roles.Include(r => r.Users).Where(predicate).ToListAsync();
        }

        public Role GetFirstEntity(Expression<Func<Role, bool>> predicate)
        {
            return repository.Roles
                .Include(r => r.Users)
                .Where(predicate)
                .FirstOrDefault();
        }

        public async Task<Role> GetFirstEntityAsync(Expression<Func<Role, bool>> predicate)
        {
            return await repository.Roles
                .Include(r => r.Users)
                .Where(predicate)
                .FirstOrDefaultAsync();
        }

        public Role GetEntryByID(int id)
        {
            return repository.Roles.Include(r => r.Users).Where(r => r.Id == id).FirstOrDefault();
        }

        public Role GetEntryByID(int id, Expression<Func<Role, bool>> predicate)
        {
            return repository.Roles.Include(r => r.Users).Where(r => r.Id == id).Where(predicate).FirstOrDefault();
        }

        public async Task<Role> GetEntryByIDAsync(int id)
        {
            return await repository.Roles.Include(r => r.Users).Where(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Role> GetEntryByIDAsync(int id, Expression<Func<Role, bool>> predicate)
        {
            return await repository.Roles.Include(r => r.Users).Where(r => r.Id == id).Where(predicate).FirstOrDefaultAsync();
        }

        public void Update(Role entity)
        {
            throw new NotImplementedException();
        }

        public async void UpdateAsync(Role entity)
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }
    }
}