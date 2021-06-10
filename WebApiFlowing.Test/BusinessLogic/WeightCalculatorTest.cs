using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using WebApiFlowing.BusinessLogic;
using WebApiFlowing.DTOs;
using WebApiFlowing.DTOs.BusinessLogic;

namespace WebApiFlowing.Test.BusinessLogic
{
    [TestFixture]
    public class WeightCalculatorTest : BaseTest
    {
        private WeightCalculator _weightCalculator;

        [SetUp]
        public void SetUp()
        {
            _weightCalculator = new WeightCalculator(_mathHelper);
        }

        [Test]
        public void EmptyUser_EstimateTargetDate_ShouldThrowArgumentNullException()
        {
            var user = new User();
            Assert.Throws<ArgumentOutOfRangeException>(() => _weightCalculator.EstimateTargetDate(user));
        }

        [Test]
        public void ValidUser_EstimateTargetDate_ShouldReturnDate()
        {
            //setup data
            var user = new User
            {
                DesiredWeightInKgs = 80,
                WeightHistories = new List<WeightHistory>
                {
                    new WeightHistory
                    {
                        DateOfMeasurement = DateTimeOffset.Now.AddDays(-2),
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
            _weightCalculator.EstimateTargetDate(user);

            //asserts
            A.CallTo(() => _mathHelper.CalculateLinearLeastSquares(A<ICollection<Point>>.That.Matches(p => ListOfPointsIsOrderedAscending(p)))).MustHaveHappened();
            A.CallTo(() => _mathHelper.FindXByY(A<LinearEquation>._, user.DesiredWeightInKgs)).MustHaveHappened();
        }

        [Test]
        public void CalculateTrend_ShouldReturnExpectedTrend()
        {
            //setup data
            var firstWeight = new WeightHistory
            {
                DateOfMeasurement = DateTimeOffset.Now.AddDays(-2),
                WeightInKgs = 100
            };
            var secondWeight = new WeightHistory
            {
                DateOfMeasurement = DateTimeOffset.Now,
                WeightInKgs = 90
            };

            var user = new User
            {
                DesiredWeightInKgs = 80,
                WeightHistories = new List<WeightHistory>
                {
                    firstWeight,
                    secondWeight
                }
            };

            //test
            _weightCalculator.CalculateTrend(user);

            //assert
            A.CallTo(() => _mathHelper.CalculateLinearLeastSquares(A<ICollection<Point>>.That.Matches(p => p.First().X == 0 
                && p.First().Y == firstWeight.WeightInKgs
                && p.Skip(1).First().X == 2
                && p.Skip(1).First().Y == secondWeight.WeightInKgs))).MustHaveHappened();
        }

        private bool ListOfPointsIsOrderedAscending(ICollection<Point> points)
        {
            var check = double.MinValue;
            foreach (var point in points)
            {
                if (point.X < check)
                    return false;

                check = point.X;
            }

            return true;
        }
    }
}