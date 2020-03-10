using TMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TMS.Persistence.Configurations
{
    public class IssueConfiguration : IEntityTypeConfiguration<Issue>
    {
        public void Configure(EntityTypeBuilder<Issue> builder)
        {
            builder.Property(t => t.Name)
               .HasMaxLength(64)
               .IsRequired();
            builder.Property(t => t.Description)
               .HasMaxLength(256);
            builder.HasOne(b => b.Assignee)
                .WithMany(d => d.AssignedIssues)
                .HasForeignKey(d => d.AssigneeId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(b => b.Reporter)
                .WithMany(d => d.ReporteredIssues)
                .HasForeignKey(d => d.ReporterId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
