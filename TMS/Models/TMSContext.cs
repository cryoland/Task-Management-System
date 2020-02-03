using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.Models
{
    public partial class TMSContext : DbContext
    {
        public DbSet<QTask> QTasks { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public TMSContext() { }
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
