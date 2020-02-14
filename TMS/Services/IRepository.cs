using System.Threading;
using System.Threading.Tasks;

namespace TMS.Services
{
    public interface IRepository
    {
        // Save DB changes synchronously.
        int Save();

        // Save DB changes asynchronously.
        Task<int> SaveAsync(CancellationToken cancellationToken = default);
    }
}
