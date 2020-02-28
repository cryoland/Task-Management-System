﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TMS.Application.Common.Mappings;
using TMS.Application.Common.Models;
using TMS.Domain.Entities;
using TMS.Domain.Enumerations;

namespace TMS.Application.Employees.Queries.GetEmployeeDetail
{
    public class EmployeeDetailVm : IMapFrom<Employee>
    {
        [HiddenInput(DisplayValue = false)]
        public long Id { get; set; }

        [Display(Name = "ShortName")]
        [Required(ErrorMessage = "Не указаны инициалы")]
        public string ShortName { get; set; }

        [Display(Name = "FullName")]
        [Required(ErrorMessage = "Не указаны ФИО")]
        [MinLength(5, ErrorMessage = "Длина ФИО от 5 символов")]
        [MaxLength(64, ErrorMessage = "Длина ФИО до 64 символов")]
        public string FullName { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Не указан Email")]
        [MinLength(5, ErrorMessage = "Длина адреса от 5 символов")]
        [MaxLength(64, ErrorMessage = "Длина адреса до 64 символов")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Не указан пароль")]
        [MinLength(5, ErrorMessage = "Длина пароля от 5 символов")]
        [MaxLength(64, ErrorMessage = "Длина пароля до 64 символов")]
        public string Password { get; set; }

        public bool Active { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        [Display(Name = "Select Role")]
        public IList<FrameDto> UserRoles =
            Enum.GetValues(typeof(UserRole))
            .Cast<UserRole>()
            .Select(r => new FrameDto { Value = (int)r, Name = r.ToString() })
            .ToList();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Employee, EmployeeDetailVm>()
                .ForMember(d => d.Id, opt => opt.MapFrom(e => e.EmployeeId))
                .ForMember(d => d.RoleName, opt => opt.MapFrom(e => e.Role.RoleValue.ToString()));
        }
    }
}
