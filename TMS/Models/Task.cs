using System.ComponentModel.DataAnnotations;

namespace TMS.Models
{
    public class QTask
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public QTaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public int? AssigneeId { get; set; }
        public Employees Assignee { get; set; }
        public int? ReporterId { get; set; }
        public Employees Reporter { get; set; }
    }
}