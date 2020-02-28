using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TMS.Domain.Entities;

namespace TMS.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Employee> Employees { get; set; }

        DbSet<Issue> Issues { get; set; }        

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}