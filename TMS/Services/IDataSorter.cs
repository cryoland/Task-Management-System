using System.Linq;

namespace TMS.Services
{
    public interface IDataSorter<T>
    {
        IQueryable<T> Sort(IQueryable<T> data, string query);
    }
}
