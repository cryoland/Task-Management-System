using TMS.Domain.Enumerations;

namespace TMS.Domain.Entities
{
    public class Issue
    {
        public int IssueId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IssueStatus Status { get; set; }
        public PriorityLevel Priority { get; set; }
        public int? AssigneeId { get; set; }
        public Employee Assignee { get; set; }
        public int? ReporterId { get; set; }
        public Employee Reporter { get; set; }
    }
}