using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiFlowing.Data.Models;

namespace WebApiFlowing.Data.Interfaces
{
    public interface IDataContext
    {
        DbSet<User> Users { get; set; }

        DbSet<BodyMassIndexRange> BodyMassIndexRanges { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}