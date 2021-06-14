using System;
using System.Threading.Tasks;
using WebApiFlowing.DTOs;

namespace WebApiFlowing.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserInfosBy(Guid guid);

        Task<User> InsertUser(User user);
    }
}