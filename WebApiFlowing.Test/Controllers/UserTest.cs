using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using WebApiFlowing.Controllers;
using WebApiFlowing.DTOs;
using WebApiFlowing.DTOs.API.Request;

namespace WebApiFlowing.Test.Controllers
{
    [TestFixture]
    public class UserTest : BaseTest
    {
        private UserController _controller;
        
        [SetUp]
        public void Setup()
        {
            _controller = new UserController(_userRepository);
        }

        [TestCase(null,   "test", 2,    100)]
        [TestCase("test", null,   2,    100)]
        [TestCase("test", "test", null, 100)]
        [TestCase("test", "test", 2,    null)]
        public void InvalidRequest_ShouldThrowValidationException(string name, string surname, double? height, double? weight)
        {
            var request = new UserRequest
            {
                Name = name,
                Surname = surname,
                HeightInMeters = height,
                DesiredWeightInKgs = weight,
                WeightHistories = new List<DTOs.API.Shared.WeightHistory>()
            };

            Assert.Throws<ValidationException>(() => Validate(request));
        }

        [Test]
        public async Task ValidRequest_ShouldInsertNewUser()
        {
            //setup
            var user = new UserRequest
            {
                Name = "diego",
                Surname = "ceccacci",
                DesiredWeightInKgs = 80,
                HeightInMeters = 2,
                WeightHistories = new List<DTOs.API.Shared.WeightHistory>
                {
                    new DTOs.API.Shared.WeightHistory
                    {
                        DateOfMeasurement = DateTimeOffset.Now.AddDays(-1),
                        WeightInKgs = 100
                    },
                    new DTOs.API.Shared.WeightHistory
                    {
                        DateOfMeasurement = DateTimeOffset.Now,
                        WeightInKgs = 90
                    }
                }
            };

            var insertedUser = new User
            {
                Guid = _defaultUserGuid,
                DesiredWeightInKgs = user.DesiredWeightInKgs.Value,
                HeightInMeters = user.HeightInMeters.Value,
                Name = user.Name,
                Surname = user.Surname,
                WeightHistories = user.WeightHistories
                    .Select(wh => new WeightHistory
                    {
                        DateOfMeasurement = wh.DateOfMeasurement,
                        WeightInKgs = wh.WeightInKgs
                    }).ToList()
            };
            A.CallTo(() => _userRepository.InsertUser(A<User>._)).Returns(insertedUser);

            //test
            var response = await _controller.Post(user);

            //assert
            A.CallTo(() => _userRepository.InsertUser(A<User>.That
                    .Matches(u => u.Name == insertedUser.Name && 
                                  u.Surname == insertedUser.Surname && 
                                  u.DesiredWeightInKgs == insertedUser.DesiredWeightInKgs &&
                                  u.HeightInMeters == insertedUser.HeightInMeters)))
                .MustHaveHappened();

            Assert.AreEqual(insertedUser.Guid, response.Guid);
        }
    }
}