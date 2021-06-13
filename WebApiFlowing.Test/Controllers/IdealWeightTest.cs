using System;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using WebApiFlowing.Controllers;
using WebApiFlowing.DTOs;

namespace WebApiFlowing.Test.Controllers
{
    [TestFixture]
    public class IdealWeightTest : BaseTest
    {
        private IdealWeightController _controller;
        
        [SetUp]
        public void Setup()
        {
            _controller = new IdealWeightController(_userRepository, _bodyMassIndexCalculator);
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
        public async Task ShouldReturnExpectedRange()
        {
            //setup
            var user = new User
            {
                HeightInMeters = 2
            };
            A.CallTo(() => _userRepository.GetUserInfosBy(_defaultUserGuid))
                .Returns(user);

            var idealWeightRange = new IdealWeightRange
            {
                MinimumIdealWeightInKgs = 10,
                MaximumIdealWeightInKgs = 20
            };
            A.CallTo(() => _bodyMassIndexCalculator.GetIdealWeightRange(user.HeightInMeters))
                .Returns(idealWeightRange);

            //test
            var response = await _controller.Get(_defaultUserGuid);

            //assert
            Assert.AreEqual(idealWeightRange.MinimumIdealWeightInKgs, response.MinimumWeightInKgs);
            Assert.AreEqual(idealWeightRange.MaximumIdealWeightInKgs, response.MaximumWeightInKgs);
        }
    }
}