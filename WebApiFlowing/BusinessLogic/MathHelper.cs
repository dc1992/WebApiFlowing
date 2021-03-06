using System;
using System.Collections.Generic;
using WebApiFlowing.BusinessLogic.Extensions;
using WebApiFlowing.BusinessLogic.Interfaces;
using WebApiFlowing.DTOs.BusinessLogic;

namespace WebApiFlowing.BusinessLogic
{
    public class MathHelper : IMathHelper
    {
        private const int MinimumNumberForCalculateALinearEquation = 2,
            NumberOfDigitsAfterCommaInFunctions = 5;

        public IMathFunction CalculateLinearLeastSquares(ICollection<Point> points)
        {
            points.ShouldContainAtLeast(MinimumNumberForCalculateALinearEquation);

            double sumOfEveryX = 0;
            double sumOfEveryY = 0;
            double sumOfXY = 0;
            double sumOfXX = 0;
            var count = points.Count;

            foreach (var point in points)
            {
                var x = point.X;
                var y = point.Y;
                sumOfEveryX += x;
                sumOfEveryY += y;
                sumOfXX += x * x;
                sumOfXY += x * y;
            }

            var m = (count * sumOfXY - sumOfEveryX * sumOfEveryY) / (count * sumOfXX - sumOfEveryX * sumOfEveryX);
            var roundedM = Math.Round(m, NumberOfDigitsAfterCommaInFunctions);
            var b = (sumOfEveryY / count) - (m * sumOfEveryX) / count;
            var roundedB = Math.Round(b, NumberOfDigitsAfterCommaInFunctions);

            var linearEquation = new LinearEquation(roundedM, roundedB);

            return linearEquation;
        }
    }
}