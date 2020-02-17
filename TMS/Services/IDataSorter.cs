using System.Collections.Generic;

namespace TMS.Services
{
    public interface IDataSorter<T>
    {
        IEnumerable<T> Sort(IEnumerable<T> data, string query);
    }
}
