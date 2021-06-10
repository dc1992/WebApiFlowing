using System;
using FakeItEasy;
using NUnit.Framework;
using WebApiFlowing.Controllers;
using WebApiFlowing.DTOs;

namespace WebApiFlowing.Test.Controllers
{
    [TestFixture]
    public class FirstAndLastTrendPointsTest : BaseTest
    {
        private FirstAndLastTrendPointsController _controller;
        
        [SetUp]
        public void Setup()
        {
            _controller = new FirstAndLastTrendPointsController(_userRepository);
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
    }
}