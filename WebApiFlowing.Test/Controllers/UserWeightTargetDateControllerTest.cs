using System;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using WebApiFlowing.Controllers;
using WebApiFlowing.DTOs;

namespace WebApiFlowing.Test.Controllers
{
    [TestFixture]
    public class UserWeightTargetDateControllerTest : BaseTest
    {
        private UserWeightTargetDateController _controller;
        
        [SetUp]
        public void Setup()
        {
            _controller = new UserWeightTargetDateController(_userRepository);
        }

        [Test]
        public async Task Get_ShouldReturnDateTimeOffset()
        {
            var response = await _controller.Get(_defaultUserGuid);

            Assert.IsTrue(response is DateTimeOffset);
        }

        [Test]
        public void NotExistingUser_ShouldThrowArgumentNullException()
        {
            A.CallTo(() => _userRepository.GetUserInfosBy(_defaultUserGuid))
                .Returns((User)null);

            Assert.ThrowsAsync<ArgumentNullException>(async () => 
                await _controller.Get(_defaultUserGuid));
        }
    }
}