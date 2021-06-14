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
                    Guid = u.Guid,
                    DesiredWeightInKgs = u.DesiredWeightInKgs,
                    HeightInMeters = u.HeightInMeters,
                    Name = u.Name,
                    Surname = u.Surname,
                    WeightHistories = u.WeightHistories.Select(wh => new WeightHistory
                    {
                        DateOfMeasurement = wh.DateOfMeasurement,
                        WeightInKgs = wh.WeightInKgs
                    }).ToList()
                })
                .SingleOrDefaultAsync(u => u.Guid == guid);

            return user;
        }

        public async Task<User> InsertUser(User user)
        {
            var userData = new Models.User
            {
                Guid = Guid.NewGuid(),
                DesiredWeightInKgs = user.DesiredWeightInKgs,
                HeightInMeters = user.HeightInMeters,
                Name = user.Name,
                Surname = user.Surname,
                WeightHistories = user.WeightHistories.Select(wh => new Data.Models.WeightHistory
                {
                    DateOfMeasurement = wh.DateOfMeasurement,
                    WeightInKgs = wh.WeightInKgs
                }).ToList()
            };

            await _dataContext.Users.AddAsync(userData);

            await _dataContext.SaveChangesAsync();

            user.Guid = userData.Guid;

            return user;
        }
    }
}