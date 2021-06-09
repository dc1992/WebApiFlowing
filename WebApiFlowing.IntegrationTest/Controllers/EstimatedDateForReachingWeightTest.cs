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
    public class EstimatedDateForReachingWeightTest : BaseTest
    {
        private EstimatedDateForReachingWeightController _controller;
        
        [SetUp]
        public void Setup()
        {
            _controller = new EstimatedDateForReachingWeightController(_userRepository, _weightCalculator);
        }

        [Test]
        public async Task ExistingUser_ShouldEstimateDate()
        {
            //setup data
            await using (var dataContext = new WebApiFlowingDataContext(_fakeDbContextOptions))
            {
                await dataContext.Users.AddAsync(new User
                {
                    Id = 1,
                    Guid = _defaultUserGuid,
                    DesiredWeightInKgs = 80,
                    HeightInMeters = 1.87,
                    Name = "Diego",
                    Surname = "Ceccacci",
                    WeightHistories = new List<WeightHistory>
                    {
                        new WeightHistory
                        {
                            Id = 1,
                            DateOfMeasurement = DateTimeOffset.Now.AddDays(-1),
                            WeightInKgs = 100
                        },
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
                });

                await dataContext.SaveChangesAsync();
            }

            //test
            var date = await _controller.Get(_defaultUserGuid);

            Assert.AreEqual(DateTimeOffset.Now.AddDays(3).Date, date.Date);
        }
    }
}