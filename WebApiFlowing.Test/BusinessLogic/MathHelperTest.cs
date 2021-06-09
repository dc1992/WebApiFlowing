using System;
using System.Collections.Generic;
using NUnit.Framework;
using WebApiFlowing.BusinessLogic;
using WebApiFlowing.DTOs.BusinessLogic;

namespace WebApiFlowing.Test.BusinessLogic
{
    [TestFixture]
    public class MathHelperTest : BaseTest
    {
        private MathHelper _mathHelper;

        [SetUp]
        public void SetUp()
        {
            _mathHelper = new MathHelper();
        }

        [Test]
        public void NotEnoughPoints_CalculateLinearLeastSquares_ShouldThrowArgumentNullException()
        {
            var listPoints = new List<Point>
            {
                new Point
                {
                    X = 0, Y = 0
                }
            };

            Assert.Throws<ArgumentOutOfRangeException>(() => _mathHelper.CalculateLinearLeastSquares(listPoints));
        }

        [Test]
        public void CalculateLinearLeastSquares1_ShouldReturnExpectedLinearFunction()
        {
            var listPoints = new List<Point>
            {
                new Point
                {
                    X = 0, Y = 0
                },
                new Point
                {
                    X = 1, Y = 1
                },
                new Point
                {
                    X = 3, Y = 3
                }
            };

            var function = _mathHelper.CalculateLinearLeastSquares(listPoints);

            Assert.AreEqual(1, function.M);
            Assert.AreEqual(0, function.B);
        }

        [Test]
        public void CalculateLinearLeastSquares2_ShouldReturnExpectedLinearFunction()
        {
            var listPoints = new List<Point>
            {
                new Point
                {
                    X = 0, Y = 0
                },
                new Point
                {
                    X = -1, Y = 1
                },
                new Point
                {
                    X = -3, Y = 3
                }
            };

            var function = _mathHelper.CalculateLinearLeastSquares(listPoints);

            Assert.AreEqual(-1, function.M);
            Assert.AreEqual(0, function.B);
        }

        [Test]
        public void CalculateLinearLeastSquares3_ShouldReturnExpectedLinearFunction()
        {
            var listPoints = new List<Point>
            {
                new Point
                {
                    X = 1, Y = 0
                },
                new Point
                {
                    X = 2, Y = 1
                },
                new Point
                {
                    X = 4, Y = 3
                }
            };

            var function = _mathHelper.CalculateLinearLeastSquares(listPoints);

            Assert.AreEqual(1, function.M);
            Assert.AreEqual(-1, function.B);
        }

        [TestCase(1, 0, 0, 0)]
        [TestCase(-1, 0, 1, -1)]
        public void FindXByY_ShouldReturnExpectedValue(double m, double b, double y, double expectedX)
        {
            var linearEquation = new LinearEquation(m, b);
            var result = _mathHelper.FindXByY(linearEquation, y);

            Assert.AreEqual(expectedX, result);
        }

        [Test]
        public void FindXByY_ResultNotFound_ShouldThrow()
        {
            var linearEquation = new LinearEquation(0, 0);
            var result = _mathHelper.FindXByY(linearEquation, 1);

            Assert.IsTrue(double.IsInfinity(result));
        }
    }
}