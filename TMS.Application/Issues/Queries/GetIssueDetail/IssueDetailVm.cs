using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TMS.Application.Common.Mappings;
using TMS.Application.Common.Models;
using TMS.Domain.Entities;
using TMS.Domain.Enumerations;

namespace TMS.Application.Issues.Queries.GetIssueDetail
{
    public class IssueDetailVm : IMapFrom<Issue>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IssueStatus Status { get; set; }

        public PriorityLevel Priority { get; set; }

        public long AssigneeId { get; set; }

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