using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMS.Models
{
    public class Employees
    {
        [Key]
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public List<QTask> AssignedQTasks { get; set; } = new List<QTask>();
        public List<QTask> ReporteredQTasks { get; set; } = new List<QTask>();
        public string Email { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
    }
} 