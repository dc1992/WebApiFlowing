using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiFlowing.Data.Interfaces;
using WebApiFlowing.DTOs;

namespace WebApiFlowing.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IDataContext _dataContext;

        public UserRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<User> GetUserInfosBy(Guid guid)
        {
            var user = await _dataContext.Users
                .AsNoTracking()
                .Select(u => new User
                {
                    Guid = u.Guid
                })
                .SingleOrDefaultAsync(u => u.Guid == guid);

            return user;
        }
    }
}