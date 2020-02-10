using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.Services
{
    public interface ITaskQueryResultSorting<T>
    {
        IQueryable<T> Sort(IQueryable<T> data, string query);
    }
}
