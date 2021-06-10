using System;
using System.Collections.Generic;
using WebApiFlowing.BusinessLogic.Extensions;
using WebApiFlowing.DTOs;
using WebApiFlowing.DTOs.BusinessLogic;

namespace WebApiFlowing.BusinessLogic
{
    public static class Mapper
    {
        private static readonly int _numberOfDigitsAfterCommaInFunctions = 5;

        public static ICollection<Point> ToPoints(this ICollection<WeightHistory> weights)
        {
            //a point is composed by X -> number of days since the first weighing, Y -> value of the weighing in kgs
            var firstWeighingDate = weights.GetFirstWeightingDate();

            var points = new List<Point>();
            foreach (var weightHistory in weights)
            {
                var point = new Point
                {
                    X = Math.Round((weightHistory.DateOfMeasurement - firstWeighingDate).TotalDays, _numberOfDigitsAfterCommaInFunctions),
                    Y = weightHistory.WeightInKgs
                };

                points.Add(point);
            }

            return points;
        }
    }
}