using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using TMS.Models;

namespace TMS.ViewModels
{
    public class TaskEditModelHybrid : TaskAddModelHybrid
    {
        public int TaskId { get; set; }

        [Display(Name = "Status")]
        public int? Status { get; set; }
        public IEnumerable<SelectListItem> StatusList { get; set; }
        public QTask Task { get; set; }
    }
}