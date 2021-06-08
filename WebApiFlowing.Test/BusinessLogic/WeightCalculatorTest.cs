﻿using System;
using System.Collections.Generic;
using FakeItEasy;
using NUnit.Framework;
using WebApiFlowing.BusinessLogic;
using WebApiFlowing.DTOs;

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
            Assert.Throws<ArgumentNullException>(() => _weightCalculator.EstimateTargetDate(user));
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