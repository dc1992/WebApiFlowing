using Microsoft.EntityFrameworkCore;
using WebApiFlowing.Data.Models;

namespace WebApiFlowing.Data.Interfaces
{
    public interface IDataContext
    {
        DbSet<User> Users { get; set; }
    }
}