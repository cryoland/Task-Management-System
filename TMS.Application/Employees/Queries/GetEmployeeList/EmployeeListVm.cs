using System.Collections.Generic;

namespace TMS.Application.Employees.Queries.GetEmployeeList
{
    public class EmployeeListVm
    {
        public IList<EmployeeDto> Employees { get; set; }
    }
}