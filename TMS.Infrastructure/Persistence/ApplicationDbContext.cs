using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TMS.Application.Common.Interfaces;
using TMS.Domain.Entities;

namespace TMS.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    { 
        public ApplicationDbContext(
            DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Issue> Issues { get; set; }

        public DbSet<Role> Roles { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}