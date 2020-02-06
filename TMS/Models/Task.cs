using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

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

    public enum QTaskStatus
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

    public static class TaskEnum
    {
        public static SelectList StatusList(int? status = null)
        {
            return new SelectList(Enum.GetValues(typeof(QTaskStatus))
                                                      .Cast<QTaskStatus>()
                                                      .Select(s => new { Value = ((int)s).ToString(), Text = s.ToString() })
                                                      .ToList(), "Value", "Text", status ?? (int)QTaskStatus.New);
        }
        public static SelectList PriorityList(int? priority = null)
        {
            return new SelectList(Enum.GetValues(typeof(TaskPriority))
                                                      .Cast<TaskPriority>()
                                                      .Select(p => new { Value = ((int)p).ToString(), Text = p.ToString() })
                                                      .ToList(), "Value", "Text", priority ?? (int)TaskPriority.Default);
        }
    }
}