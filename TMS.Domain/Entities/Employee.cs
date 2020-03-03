using System;
using System.Collections.Generic;
using TMS.Domain.Enumerations;

namespace TMS.Domain.Entities
{
    public class Employee
    {
        public long EmployeeId { get; set; }
        public string AppUserId { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public IEnumerable<Issue> AssignedIssues { get; private set; } = new HashSet<Issue>();
        public IEnumerable<Issue> ReporteredIssues { get; private set; } = new HashSet<Issue>();
    }
}
