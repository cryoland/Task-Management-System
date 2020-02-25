using TMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TMS.Infrastructure.Persistence.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(t => t.Email)
                .HasMaxLength(80)
                .IsRequired();
            builder.Property(t => t.Password)
                .IsRequired();
            builder.Property(t => t.FullName)
                .HasMaxLength(100)
                .IsRequired();
            builder.HasOne(e => e.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(e => e.RoleId);
        }
    }
}