using TMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TMS.Infrastructure.Persistence.Configurations
{
    public class IssueConfiguration : IEntityTypeConfiguration<Issue>
    {
        public void Configure(EntityTypeBuilder<Issue> builder)
        {
            builder.Property(t => t.Name)
               .HasMaxLength(80)
               .IsRequired();
            builder.Property(t => t.Description)
               .HasMaxLength(1024);
        }
    }
}