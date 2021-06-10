using System;
using System.Collections.Generic;
using NUnit.Framework;
using WebApiFlowing.BusinessLogic.Extensions;
using WebApiFlowing.DTOs;

namespace WebApiFlowing.Test.BusinessLogic
{
    [TestFixture]
    public class WeightHistoryTest
    {
        [Test]
        public void GetFirstWeightingDate_ShouldReturnFirstDate()
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
            var firstPoint = orderedWeights.GetFirstWeightingDate();
            Assert.AreEqual(firstWeight.DateOfMeasurement, firstPoint);
        }
    }
}