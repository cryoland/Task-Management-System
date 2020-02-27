using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TMS.Application.Common.Mappings;
using TMS.Application.Common.Models;
using TMS.Domain.Entities;
using TMS.Domain.Enumerations;

namespace TMS.Application.Employees.Queries.GetEmployeeDetail
{
    public class EmployeeDetailVm : IMapFrom<Employee>
    {
        public long Id { get; set; }

        public string ShortName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool Active { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public IList<FrameDto> UserRoles =
            Enum.GetValues(typeof(UserRole))
            .Cast<UserRole>()
            .Select(r => new FrameDto { Value = (int)r, Name = r.ToString() })
            .ToList();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Employee, EmployeeDetailVm>()
                .ForMember(d => d.Id, opt => opt.MapFrom(e => e.EmployeeId))
                .ForMember(e => e.Active, opt => opt.Ignore())
                .ForMember(d => d.RoleName, opt => opt.MapFrom(e => e.Role.RoleValue.ToString()));
        }
    }
}