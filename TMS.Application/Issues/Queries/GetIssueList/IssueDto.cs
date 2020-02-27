using AutoMapper;
using TMS.Application.Common.Mappings;
using TMS.Domain.Entities;


namespace TMS.Application.Issues.Queries.GetIssueList
{
    public class IssueDto : IMapFrom<Issue>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string AssigneeName { get; set; }

        public string ReporterName { get; set; }

        public string StatusName { get; set; }

        public string PriorityName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Issue, IssueDto>()
                .ForMember(dto => dto.Id, exp => exp.MapFrom(i => i.IssueId))
                .ForMember(dto => dto.StatusName, exp => exp.MapFrom(i => i.Status.ToString()))
                .ForMember(dto => dto.PriorityName, exp => exp.MapFrom(i => i.Priority.ToString()))
                .ForMember(dto => dto.AssigneeName, exp => exp.MapFrom(i => i.Assignee.FullName))
                .ForMember(dto => dto.ReporterName, exp => exp.MapFrom(i => i.Reporter.FullName));
        }
    }
}