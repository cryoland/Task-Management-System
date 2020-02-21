using AutoMapper;
using TMS.Application.Common.Mappings;
using TMS.Domain.Entities;

namespace TMS.Application.Employees.Queries.GetEmployeeDetail
{
    public class EmployeeDetailVm : IMapFrom<Employee>
    {
        public int EmployeeId { get; set; }

        public string ShortName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool Active { get; set; }

        public int RoleId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Employee, EmployeeDetailVm>()
                .ForMember(e => e.Active, opt => opt.Ignore());
        }
    }
}