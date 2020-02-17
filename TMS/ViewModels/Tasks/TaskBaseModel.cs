using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace TMS.ViewModels.Tasks
{
    public abstract class TaskBaseModel
    {
        protected TaskBaseModel() { }
        public IEnumerable<SelectListItem> AssigneeList { get; set; }
        public IEnumerable<SelectListItem> ReporterList { get; set; }
        public IEnumerable<SelectListItem> PriorityList { get; set; }
    }
}
