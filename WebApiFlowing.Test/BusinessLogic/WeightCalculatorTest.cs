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
        private WeightTrendCalculator _weightCalculator;

        [SetUp]
        public void SetUp()
        {
            _weightCalculator = new WeightTrendCalculator(_mathHelper);
        }

        [Test]
        public void EmptyUser_EstimateTargetDate_ShouldThrowArgumentNullException()
        {
            var user = new User();
            Assert.Throws<ArgumentOutOfRangeException>(() => _weightCalculator.EstimateTarget(user));
        }

        [Test]
        public void ValidUser_EstimateTarget_ShouldReturnExpectedTarget()
        {
            //setup data
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
                DesiredWeightInKgs = 80,
                WeightHistories = new List<WeightHistory>
                {
                    firstWeight,
                    secondWeight
                }
            };

            var linearEquation = new LinearEquation(-10, 100);
            A.CallTo(() => _mathHelper.CalculateLinearLeastSquares(A<ICollection<Point>>._))
                .Returns(linearEquation);

            //test
            _weightCalculator.EstimateTarget(user);

            //asserts
            A.CallTo(() => _mathHelper.CalculateLinearLeastSquares(A<ICollection<Point>>.That.Matches(p => ListOfPointsIsOrderedAscending(p))))
                .MustHaveHappened();

            A.CallTo(() => _mathHelper.CalculateLinearLeastSquares(A<ICollection<Point>>.That.Matches(p => p.First().X == 0
                && p.First().Y == firstWeight.WeightInKgs
                && p.Skip(1).First().X == (secondWeight.DateOfMeasurement - firstWeight.DateOfMeasurement).Days
                && p.Skip(1).First().Y == secondWeight.WeightInKgs))).MustHaveHappened();
        }

        [Test]
        public void TargetDateInThePast_ShouldThrowArgumentOutOfRangeException()
        {
            //setup data
            var user = new User
            {
                DesiredWeightInKgs = 95,
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

            var linearEquation = new LinearEquation(-10, 100);
            A.CallTo(() => _mathHelper.CalculateLinearLeastSquares(A<ICollection<Point>>._))
                .Returns(linearEquation);

            //test
            Assert.Throws<ArgumentOutOfRangeException>(() => _weightCalculator.EstimateTarget(user));
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