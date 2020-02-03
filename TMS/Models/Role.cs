using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.Models
{
    public class Role
    {
        public int Id { get; set; }
        public EmployeeRole Name { get; set; }
        public List<Employees> Users { get; set; } = new List<Employees>();
    }
}