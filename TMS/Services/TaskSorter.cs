using System.Collections.Generic;
using System.Linq;
using TMS.Models;

namespace TMS.Services
{
    public class TaskSorter : IDataSorter<QTask>
    {
        public IEnumerable<QTask> Sort(IEnumerable<QTask> result, string query)
        {
            switch (query)
            {
                case TaskSort.PriotityAsc:
                    result = result.OrderBy(p => p.Priority);
                    break;
                case TaskSort.PriotityDesc:
                    result = result.OrderByDescending(p => p.Priority);
                    break;
                case TaskSort.StatusAsc:
                    result = result.OrderBy(p => p.Status);
                    break;
                case TaskSort.StatusDesc:
                    result = result.OrderByDescending(p => p.Status);
                    break;
                case TaskSort.NameDesc:
                    result = result.OrderByDescending(p => p.Name);
                    break;
                default:
                    break;
            }
            return result;
        }
    }

    internal static class TaskSort
    {
        public const string PriotityAsc = "priority_asc";
        public const string PriotityDesc = "priority_desc";
        public const string StatusAsc = "status_asc";
        public const string StatusDesc = "status_desc";
        public const string NameDesc = "name_desc";
    }
}
