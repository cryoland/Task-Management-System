using Microsoft.EntityFrameworkCore;
using TMS.Models;

namespace TMS.Services
{
    public interface ITMSRepository : IRepository
    {
        DbSet<QTask> QTasks { get; set; }
        DbSet<Employees> Employees { get; set; }
        DbSet<Role> Roles { get; set; }
    }
}