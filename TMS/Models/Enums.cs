using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TMS.Models
{
    public enum EmployeeRole
    {
        Admin = 0,
        Manager = 1,
        Developer = 2,
    }

    public enum QTaskStatus
    {
        None = -1,
        New = 0,
        InProgress = 1,
        Done = 2
    }

    public enum TaskPriority
    {
        None = -1,
        Default = 0,
        Major = 1
    }

    public static class EmployeeEnum
    {
        public static SelectList RoleList(int? role = null)
        {
            return new SelectList(Enum.GetValues(typeof(EmployeeRole))
                                                      .Cast<EmployeeRole>()
                                                      .Select(r => new { Value = ((int)r).ToString(), Text = r.ToString() })
                                                      .ToList(), "Value", "Text", role ?? (int)EmployeeRole.Developer);
        }
    }

    public static class TaskEnum
    {
        public static SelectList StatusList(int? status = null)
        {
            return new SelectList(Enum.GetValues(typeof(QTaskStatus))
                                                      .Cast<QTaskStatus>()
                                                      .Where(s => !s.Equals(QTaskStatus.None))
                                                      .Select(s => new { Value = ((int)s).ToString(), Text = s.ToString() })
                                                      .ToList(), "Value", "Text", status ?? (int)QTaskStatus.New);
        }
        public static SelectList PriorityList(int? priority = null)
        {
            return new SelectList(Enum.GetValues(typeof(TaskPriority))
                                                      .Cast<TaskPriority>()
                                                      .Where(p => !p.Equals(TaskPriority.None))
                                                      .Select(p => new { Value = ((int)p).ToString(), Text = p.ToString() })
                                                      .ToList(), "Value", "Text", priority ?? (int)TaskPriority.Default);
        }
    }

    internal static class TaskSort
    {
        public const string PriotityAsc = "priority_asc";
        public const string PriotityDesc = "priority_desc";
        public const string StatusAsc = "status_asc";
        public const string StatusDesc = "status_desc";
        public const string NameDesc = "name_desc";
    }
}