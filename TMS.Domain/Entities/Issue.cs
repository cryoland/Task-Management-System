using TMS.Domain.Common;
using TMS.Domain.Enumerations;

namespace TMS.Domain.Entities
{
    public class Issue : AuditableEntity
    {
        public long IssueId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IssueStatus Status { get; set; }

        public PriorityLevel Priority { get; set; }

        public long? AssigneeId { get; set; }

        public Employee Assignee { get; set; }

        public long? ReporterId { get; set; }

        public Employee Reporter { get; set; }
    }
}
