using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TMS.Application.Common.Mappings;
using TMS.Application.Common.Models;
using TMS.Domain.Entities;
using TMS.Domain.Enumerations;

namespace TMS.Application.Issues.Queries.GetIssueDetail
{
    public class IssueDetailVm : IMapFrom<Issue>
    {
        [HiddenInput(DisplayValue = false)]
        public long Id { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Не указано название")]
        [MinLength(5, ErrorMessage = "Длина названия от 5 символов")]
        [MaxLength(64, ErrorMessage = "Длина названия до 64 символов")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [MinLength(5, ErrorMessage = "Длина описания от 5 символов")]
        [MaxLength(256, ErrorMessage = "Длина описания до 256 символов")]
        public string Description { get; set; }

        [Display(Name = "Status")]
        public IssueStatus Status { get; set; }

        [Display(Name = "Priority")]
        [Required(ErrorMessage = "Не указан приоритет")]
        public PriorityLevel Priority { get; set; }

        [Display(Name = "Select assignee")]
        [Required(ErrorMessage = "Не указан исполнитель")]
        public long AssigneeId { get; set; }

        [Display(Name = "Select reporter")]
        [Required(ErrorMessage = "Не указан автор")]
        public long ReporterId { get; set; }

        public string AssigneeName { get; set; }

        public string ReporterName { get; set; }

        public IList<FrameDto> IssueStatuses =
            Enum.GetValues(typeof(IssueStatus))
            .Cast<IssueStatus>()
            .Select(s => new FrameDto { Value = (int)s, Name = s.ToString() })
            .ToList();

        public IList<FrameDto> IssuePriorityLevels =
            Enum.GetValues(typeof(PriorityLevel))
            .Cast<PriorityLevel>()
            .Select(p => new FrameDto { Value = (int)p, Name = p.ToString() })
            .ToList();

        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap<Issue, IssueDetailVm>()
                .ForMember(vm => vm.Id, exp => exp.MapFrom(i => i.IssueId))
                .ForMember(dto => dto.AssigneeName, exp => exp.MapFrom(i => i.Assignee.FullName))
                .ForMember(dto => dto.ReporterName, exp => exp.MapFrom(i => i.Reporter.FullName));
        }
    }
}
