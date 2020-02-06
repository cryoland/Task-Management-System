using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using TMS.Models;

namespace TMS.ViewModels
{
    public class TaskEditModelHybrid
    {
        public int TaskId { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Select assignee")]
        public int? AssigneeId { get; set; }

        [Display(Name = "Select reporter")]
        public int? ReporterId { get; set; }

        [Display(Name = "Priority")]
        public int? Priority { get; set; }

        [Display(Name = "Status")]
        public int? Status { get; set; }

        public IEnumerable<SelectListItem> AssigneeList { get; set; }
        public IEnumerable<SelectListItem> ReporterList { get; set; }
        public IEnumerable<SelectListItem> PriorityList { get; set; }
        public IEnumerable<SelectListItem> StatusList { get; set; }
        public QTask Task { get; set; }
    }
}