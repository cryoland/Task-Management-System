using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TMS.Services;

namespace TMS.Models
{
    public partial class TMSRepository : DbContext, ITMSRepository
    {
        public DbSet<QTask> QTasks { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public TMSRepository() { }
        public TMSRepository(DbContextOptions<TMSRepository> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }
        public int Save()
        {
            return base.SaveChanges();
        }
        public Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QTask>()
                .HasOne<Employees>(e => e.Assignee)
                .WithMany(d => d.AssignedQTasks)
                .HasForeignKey(e => e.AssigneeId);

            modelBuilder.Entity<QTask>()
                .HasOne<Employees>(e => e.Reporter)
                .WithMany(d => d.ReporteredQTasks)
                .HasForeignKey(e => e.ReporterId);

            modelBuilder.Entity<Employees>()
                .HasOne<Role>(r => r.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(e => e.RoleId);
        }
    }
}
