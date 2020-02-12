using System.Collections.Generic;
using TMS.Models;

namespace TMS.ViewModels
{
    public class TaskIndexModel
    {
        public IEnumerable<QTask> ReporterTaskList { get; set; }
        public IEnumerable<QTask> AssigneeTaskList { get; set; }
        public IEnumerable<QTask> OtherTaskList { get; set; }
        public string NameSort { get; set; }
        public string PrioritySort { get; set; }
        public string StatusSort { get; set; }
    }
}
