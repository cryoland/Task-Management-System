using System.Collections.Generic;
using TMS.Domain.Enumerations;

namespace TMS.Domain.Entities
{
    public class Employee
    {
        public long EmployeeId { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public UserRole Role { get; set; }
        public IEnumerable<Issue> AssignedIssues { get; private set; } = new HashSet<Issue>();
        public IEnumerable<Issue> ReporteredIssues { get; private set; } = new HashSet<Issue>();
    }
}