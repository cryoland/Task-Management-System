using System.Collections.Generic;
using TMS.Domain.Enumerations;

namespace TMS.Domain.Entities
{
    public class Role
    {
        public int RoleId { get; set; }
        public UserRole RoleValue { get; set; }
        public IEnumerable<Employee> Users { get; set; } = new HashSet<Employee>();
    }
}