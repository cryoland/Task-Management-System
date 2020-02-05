using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TMS.ViewModels
{
    public class TaskEditModel
    {
        public int TaskId { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Не указано название")]
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
    }
}
