using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using WebApiFlowing.BusinessLogic;
using WebApiFlowing.DTOs;

namespace WebApiFlowing.Test.BusinessLogic
{
    [TestFixture]
    public class MapperTest
    {
        [Test]
        public void ToPoints_ShouldReturnExpectedData()
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
            var thirdWeight = new WeightHistory
            {
                DateOfMeasurement = DateTimeOffset.Now,
                WeightInKgs = 80
            };

            var orderedWeights = new List<WeightHistory>
            {
                firstWeight,
                secondWeight,
                thirdWeight
            };

            //test
            var points = orderedWeights.ToPoints();

            //assert
            var firstPoint = points.First();
            var secondPoint = points.Skip(1).First();
            var thirdPoint = points.Skip(2).First();

            Assert.AreEqual(0m, firstPoint.X);
            Assert.AreEqual(firstWeight.WeightInKgs, firstPoint.Y);
            Assert.AreEqual(1m, secondPoint.X);
            Assert.AreEqual(secondWeight.WeightInKgs, secondPoint.Y);
            Assert.AreEqual(2m, thirdPoint.X);
            Assert.AreEqual(thirdWeight.WeightInKgs, thirdPoint.Y);
        }
    }
}