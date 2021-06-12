using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
                WeightHistories = new List<WeightHistory>()
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
                WeightHistories = new List<WeightHistory>
                {
                    new WeightHistory
                    {
                        DateOfMeasurement = DateTimeOffset.Now.AddDays(-1),
                        WeightInKgs = 100
                    },
                    new WeightHistory
                    {
                        DateOfMeasurement = DateTimeOffset.Now,
                        WeightInKgs = 90
                    }
                }
            };

            //test
            await _controller.Post(user);

            //assert
            A.CallTo(() => _userRepository.InsertUser(A<User>.That
                    .Matches(u => u.Name == user.Name && 
                                  u.Surname == user.Surname && 
                                  u.DesiredWeightInKgs == user.DesiredWeightInKgs.Value &&
                                  u.HeightInMeters == user.HeightInMeters.Value &&
                                  u.WeightHistories.Count == user.WeightHistories.Count)))
                .MustHaveHappened();
        }
    }
}