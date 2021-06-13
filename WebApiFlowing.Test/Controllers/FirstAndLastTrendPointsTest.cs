using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using WebApiFlowing.BusinessLogic.Extensions;
using WebApiFlowing.Controllers;
using WebApiFlowing.DTOs;
using WebApiFlowing.DTOs.BusinessLogic;

namespace WebApiFlowing.Test.Controllers
{
    [TestFixture]
    public class FirstAndLastTrendPointsTest : BaseTest
    {
        private FirstAndLastTrendPointsController _controller;
        
        [SetUp]
        public void Setup()
        {
            _controller = new FirstAndLastTrendPointsController(_userRepository, _weightCalculator, _mathHelper);
        }

        [Test]
        public async Task ExistingUser_ShouldReturnExpected()
        {
            //setup
            var firstWeight = new WeightHistory
            {
                DateOfMeasurement = DateTimeOffset.Now.AddDays(-2),
                WeightInKgs = 100
            };
            
            var secondWeight = new WeightHistory
            {
                DateOfMeasurement = DateTimeOffset.Now.AddDays(-1),
                WeightInKgs = 90
            };

            var user = new User
            {
                Guid = _defaultUserGuid,
                DesiredWeightInKgs = 80,
                WeightHistories = new List<WeightHistory>
                {
                    firstWeight,
                    secondWeight
                }
            };

            A.CallTo(() => _userRepository.GetUserInfosBy(_defaultUserGuid))
                .Returns(user);

            var equation = new LinearEquation(10, 20);
            var expectedFirstPointY = 10;    

            A.CallTo(() => _weightCalculator.CalculateTrend(user)).Returns(equation);
            A.CallTo(() => _mathHelper.FindZero(equation)).Returns(expectedFirstPointY);

            var findXByYResult = 10;
            A.CallTo(() => _mathHelper.FindXByY(equation, user.DesiredWeightInKgs)).Returns(findXByYResult);

            var expectedLastPointX = user.WeightHistories.GetFirstWeightingDate().AddDays(findXByYResult);

            //test
            var result = await _controller.Get(_defaultUserGuid);

            //assert
            Assert.AreEqual(firstWeight.DateOfMeasurement, result.FirstTrendPoint.X);
            Assert.AreEqual(expectedFirstPointY, result.FirstTrendPoint.Y);

            Assert.AreEqual(expectedLastPointX, result.LastTrendPoint.X);
            Assert.AreEqual(user.DesiredWeightInKgs, result.LastTrendPoint.Y);
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
        public void EstimatedDateBeforeLastDate_ShouldThrowArgumentOutOfRangeException()
        {
            //setup
            var firstWeight = new WeightHistory
            {
                DateOfMeasurement = DateTimeOffset.Now.AddDays(-20),
                WeightInKgs = 100
            };

            var secondWeight = new WeightHistory
            {
                DateOfMeasurement = DateTimeOffset.Now.AddDays(-1),
                WeightInKgs = 90
            };

            var user = new User
            {
                Guid = _defaultUserGuid,
                DesiredWeightInKgs = 150,
                WeightHistories = new List<WeightHistory>
                {
                    firstWeight,
                    secondWeight
                }
            };

            A.CallTo(() => _userRepository.GetUserInfosBy(_defaultUserGuid))
                .Returns(user);

            var equation = new LinearEquation(10, 20);
            var expectedFirstPointY = 10;

            A.CallTo(() => _weightCalculator.CalculateTrend(user)).Returns(equation);
            A.CallTo(() => _mathHelper.FindZero(equation)).Returns(expectedFirstPointY);

            var findXByYResult = 10;
            A.CallTo(() => _mathHelper.FindXByY(equation, user.DesiredWeightInKgs)).Returns(findXByYResult);

            //test
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>  await _controller.Get(_defaultUserGuid));
        }
    }
}