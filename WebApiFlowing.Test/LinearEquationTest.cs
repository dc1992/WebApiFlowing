using NUnit.Framework;
using WebApiFlowing.BusinessLogic;

namespace WebApiFlowing.Test
{
    [TestFixture]
    public class LinearEquationTest
    {
        [TestCase(1, 2, "y = 1x + 2")]
        [TestCase(5, 2, "y = 5x + 2")]
        public void ToString_ShouldReturnExpectedValue(double m, double b, string expected)
        {
            var linearFunction = new LinearEquation(m, b);

            Assert.AreEqual(expected, linearFunction.ToString());
        }
    }
}