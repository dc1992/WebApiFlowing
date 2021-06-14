using System;
using System.Collections.Generic;
using System.Linq;
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

            //assert
            Assert.AreEqual(firstWeight.DateOfMeasurement, firstPoint);
        }

        [Test]
        public void GetOrderedCollection_ShouldReturnOrderedCollection()
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
                secondWeight,
                firstWeight,
                thirdWeight
            };

            //test
            var orderedCollection = orderedWeights.GetOrderedCollection();

            //assert
            Assert.AreEqual(firstWeight.DateOfMeasurement, orderedCollection.First().DateOfMeasurement);
            Assert.AreEqual(firstWeight.WeightInKgs, orderedCollection.First().WeightInKgs);

            Assert.AreEqual(secondWeight.DateOfMeasurement, orderedCollection.Skip(1).First().DateOfMeasurement);
            Assert.AreEqual(secondWeight.WeightInKgs, orderedCollection.Skip(1).First().WeightInKgs);

            Assert.AreEqual(thirdWeight.DateOfMeasurement, orderedCollection.Skip(2).First().DateOfMeasurement);
            Assert.AreEqual(thirdWeight.WeightInKgs, orderedCollection.Skip(2).First().WeightInKgs);
        }

        [Test]
        public void GetLastWeightingDate_ShouldReturnLastDate()
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
            var lastPoint = orderedWeights.GetLastWeightingDate();

            //assert
            Assert.AreEqual(thirdWeight.DateOfMeasurement, lastPoint);
        }
    }
}