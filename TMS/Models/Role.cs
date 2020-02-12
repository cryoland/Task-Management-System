using System.Collections.Generic;

namespace TMS.Models
{
    public class Role
    {
        public int Id { get; set; }
        public EmployeeRole Name { get; set; }
        public List<Employees> Users { get; set; } = new List<Employees>();
    }
}