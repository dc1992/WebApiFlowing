using System;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using WebApiFlowing.Controllers;
using WebApiFlowing.DTOs;

namespace WebApiFlowing.Test.Controllers
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

        [Test]
        public async Task ExistingUser_ShouldEstimateDate()
        {
            await _controller.Get(_defaultUserGuid);

            A.CallTo(() => _userRepository.GetUserInfosBy(_defaultUserGuid)).MustHaveHappened();
            A.CallTo(() => _weightCalculator.EstimateTargetDate(A<User>._)).MustHaveHappened();
        }
    }
}