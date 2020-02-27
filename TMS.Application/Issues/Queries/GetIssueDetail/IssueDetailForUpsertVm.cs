using AutoMapper;
using System.Collections.Generic;
using TMS.Application.Common.Models;
using TMS.Domain.Entities;

namespace TMS.Application.Issues.Queries.GetIssueDetail
{
    public class IssueDetailForUpsertVm : IssueDetailVm
    {
        public IList<FrameDto> Employees { get; set; }

        public override void Mapping(Profile profile)
        {
            profile.CreateMap<Issue, IssueDetailForUpsertVm>()
                .ForMember(vm => vm.Id, exp => exp.MapFrom(i => i.IssueId))
                .ForMember(dto => dto.AssigneeName, exp => exp.MapFrom(i => i.Assignee.FullName))
                .ForMember(dto => dto.ReporterName, exp => exp.MapFrom(i => i.Reporter.FullName));
        }
    }
}