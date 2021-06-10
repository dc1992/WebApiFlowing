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

        public LinearEquation CalculateLinearLeastSquares(ICollection<Point> points)
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

        public double FindXByY(LinearEquation linearEquation, double y)
        {
            var x = (y - linearEquation.B) / linearEquation.M;

            return x;
        }

        public double FindZero(LinearEquation linearEquation)
        {
            //we can ignore the mx since x is zero -> y = (linearEquation.M * 0) + b
            var y = linearEquation.B;

            return y;
        }
    }
}