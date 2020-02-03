using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS.Models
{
    public class QTask
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public int? AssigneeId { get; set; }
        public Employees Assignee { get; set; }
        public int? ReporterId { get; set; }
        public Employees Reporter { get; set; }
    }

    public enum TaskStatus
    {
        New = 0,
        InProgress = 1,
        Done = 2
    }

    public enum TaskPriority
    {
        Default = 0,
        Major = 1
    }
}
