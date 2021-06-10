using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using WebApiFlowing.Controllers;
using WebApiFlowing.Data;
using WebApiFlowing.Data.Models;

namespace WebApiFlowing.IntegrationTest.Controllers
{
    [TestFixture]
    public class FirstAndLastTrendPointsTest : BaseTest
    {
        private FirstAndLastTrendPointsController _controller;
        
        [SetUp]
        public void Setup()
        {
            _controller = new FirstAndLastTrendPointsController(_userRepository, _mathHelper);
        }

        [Test]
        public async Task ExistingUser_ShouldReturnExpectedPoints()
        {
            //setup data
            var firstWeight = new WeightHistory
            {
                Id = 1,
                DateOfMeasurement = DateTimeOffset.Now.AddDays(-1),
                WeightInKgs = 100
            };
            var user = new User
            {
                Id = 1,
                Guid = _defaultUserGuid,
                DesiredWeightInKgs = 80,
                HeightInMeters = 1.87,
                Name = "Diego",
                Surname = "Ceccacci",
                WeightHistories = new List<WeightHistory>
                {
                    firstWeight,
                    new WeightHistory
                    {
                        Id = 2,
                        DateOfMeasurement = DateTimeOffset.Now,
                        WeightInKgs = 95
                    },
                    new WeightHistory
                    {
                        Id = 3,
                        DateOfMeasurement = DateTimeOffset.Now.AddDays(1),
                        WeightInKgs = 90
                    }
                }
            };

            await using (var dataContext = new WebApiFlowingDataContext(_fakeDbContextOptions))
            {
                await dataContext.Users.AddAsync(user);

                await dataContext.SaveChangesAsync();
            }

            //test
            var response = await _controller.Get(_defaultUserGuid);

            //assert
            Assert.AreEqual(firstWeight.DateOfMeasurement, response.FirstTrendPoint.X);
            Assert.AreEqual(100, response.FirstTrendPoint.Y);
            Assert.AreEqual(DateTimeOffset.Now.AddDays(3).Date, response.LastTrendPoint.X.Date);
            Assert.AreEqual(user.DesiredWeightInKgs, response.LastTrendPoint.Y);
        }
    }
}