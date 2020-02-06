using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TMS.Models;

namespace TMS.ViewModels
{
    public class TaskAddModelHybrid
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Не указано название")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Select assignee")]
        [Required(ErrorMessage = "Не указан исполнитель")]
        public int AssigneeId { get; set; }

        [Display(Name = "Select reporter")]
        [Required(ErrorMessage = "Не указан автор")]
        public int ReporterId { get; set; }

        [Display(Name = "Priority")]
        [Required(ErrorMessage = "Не указан приоритет")]
        public int Priority { get; set; }

        public IEnumerable<SelectListItem> AssigneeList { get; set; }
        public IEnumerable<SelectListItem> ReporterList { get; set; }
        public IEnumerable<SelectListItem> PriorityList { get; set; }
    }
}