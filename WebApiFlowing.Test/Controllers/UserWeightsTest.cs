using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using WebApiFlowing.Controllers;
using WebApiFlowing.DTOs;

namespace WebApiFlowing.Test.Controllers
{
    [TestFixture]
    public class UserWeightsTest : BaseTest
    {
        private UserWeightsController _controller;
        
        [SetUp]
        public void Setup()
        {
            _controller = new UserWeightsController(_userRepository);
        }

        [Test]
        public void ExistingUser_ShouldNotThrow()
        {
            Assert.DoesNotThrowAsync(async () => await _controller.Get(_defaultUserGuid));
        }

        [Test]
        public void NotExistingUser_ShouldThrowArgumentNullException()
        {
            A.CallTo(() => _userRepository.GetUserInfosBy(_defaultUserGuid))
                .Returns((User)null);

            Assert.ThrowsAsync<ArgumentNullException>(async () => 
                await _controller.Get(_defaultUserGuid));
        }

        [Test]
        public async Task ExistingUser_ShouldReturnListOfWeights()
        {
            //setup
            var firstWeight = new WeightHistory
            {
                DateOfMeasurement = DateTimeOffset.Now.AddDays(-1),
                WeightInKgs = 100
            };
            var secondWeight = new WeightHistory
            {
                DateOfMeasurement = DateTimeOffset.Now,
                WeightInKgs = 90
            };

            var user = new User
            {
                WeightHistories = new List<WeightHistory>
                {
                    firstWeight,
                    secondWeight
                }
            };

            A.CallTo(() => _userRepository.GetUserInfosBy(_defaultUserGuid))
                .Returns(user);

            //test
            var result = await _controller.Get(_defaultUserGuid);

            //assert
            A.CallTo(() => _userRepository.GetUserInfosBy(_defaultUserGuid)).MustHaveHappened();
            Assert.AreEqual(user.WeightHistories.Count, result.WeightHistories.Count);
            
            var firstResultingWeight = result.WeightHistories.First();
            Assert.AreEqual(firstWeight.DateOfMeasurement, firstResultingWeight.DateOfMeasurement);
            Assert.AreEqual(firstWeight.WeightInKgs, firstResultingWeight.WeightInKgs);

            var secondResultingWeight = result.WeightHistories.Skip(1).First();
            Assert.AreEqual(secondWeight.DateOfMeasurement, secondResultingWeight.DateOfMeasurement);
            Assert.AreEqual(secondWeight.WeightInKgs, secondResultingWeight.WeightInKgs);
        }
    }
}