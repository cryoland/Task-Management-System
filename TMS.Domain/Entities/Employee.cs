using System.Collections.Generic;

namespace TMS.Domain.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public IEnumerable<Issue> AssignedQTasks { get; private set; } = new HashSet<Issue>();
        public IEnumerable<Issue> ReporteredQTasks { get; private set; } = new HashSet<Issue>();
    }
}