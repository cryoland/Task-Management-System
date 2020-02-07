using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.Models;

namespace TMS.ViewModels
{
    public class TaskIndexModel
    {
        public IEnumerable<QTask> TaskList { get; set; }
        public string NameSort { get; set; }
        public string PrioritySort { get; set; }
        public string StatusSort { get; set; }
    }
}
