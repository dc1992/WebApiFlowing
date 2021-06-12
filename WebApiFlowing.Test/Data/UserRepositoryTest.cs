using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using WebApiFlowing.Data;
using WebApiFlowing.Data.Models;
using WebApiFlowing.Data.Repositories;
using WeightHistory = WebApiFlowing.DTOs.WeightHistory;

namespace WebApiFlowing.Test.Data
{
    [TestFixture]
    public class UserRepositoryTest : BaseTest
    {
        private UserRepository _userRepository;

        [SetUp]
        public void SetUp()
        {
            _userRepository = new UserRepository(_dataContext);
        }

        [Test]
        public async Task ExisingUser_GetUserInfosBy_ShouldReturnExpected()
        {
            //setup user data
            await using (var setupContext = new WebApiFlowingDataContext(_options))
            {
                await setupContext.Users.AddAsync(new User
                {
                    Guid = _defaultUserGuid,
                    Id = 1
                });

                await setupContext.SaveChangesAsync();
            }

            //test execution
            var user = await _userRepository.GetUserInfosBy(_defaultUserGuid);

            Assert.AreEqual(_defaultUserGuid, user.Guid);
        }

        [Test]
        public async Task NotExistingUser_GetUserInfosBy_ShouldReturnExpected()
        {
            //test execution
            var user = await _userRepository.GetUserInfosBy(_defaultUserGuid);

            Assert.IsNull(user);
        }

        [Test]
        public async Task InsertUser_ShouldInsertUser()
        {
            //test
            var userToInsert = new DTOs.User
            {
                WeightHistories = new List<WeightHistory>(),
                HeightInMeters = 2,
                Name = "test",
                Surname = "test2",
                DesiredWeightInKgs = 80
            };

            var inserted = await _userRepository.InsertUser(userToInsert);

            //assert
            var dataContextUser = await _dataContext.Users
                .SingleOrDefaultAsync(u => u.Guid == inserted.Guid &&
                   u.Name == userToInsert.Name &&
                   u.Surname == userToInsert.Surname &&
                   u.DesiredWeightInKgs == userToInsert.DesiredWeightInKgs &&
                   u.HeightInMeters == userToInsert.HeightInMeters);
                    
            Assert.IsNotNull(dataContextUser);
        }
    }
}