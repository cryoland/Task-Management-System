using System.Linq;

namespace TMS.Services
{
    public interface ITaskQueryResultSorting<T>
    {
        IQueryable<T> Sort(IQueryable<T> data, string query);
    }
}
