using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TMS.Models
{
    public interface IManualDataContext
    {
        DbSet<QTask> QTasks { get; set; }
        DbSet<Employees> Employees { get; set; }
        DbSet<Role> Roles { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

    public partial class TMSContext : DbContext, IManualDataContext
    {
        public DbSet<QTask> QTasks { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public TMSContext() { }
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync();
        }
        public TMSContext(DbContextOptions<TMSContext> options)
            : base(options)
        {
            Database.EnsureCreated();
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
