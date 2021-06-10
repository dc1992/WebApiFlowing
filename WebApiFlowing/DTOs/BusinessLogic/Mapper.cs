using System.Collections.Generic;
using WebApiFlowing.BusinessLogic.Extensions;

namespace WebApiFlowing.DTOs.BusinessLogic
{
    public static class Mapper
    {
        public static ICollection<Point> ToPoints(this ICollection<WeightHistory> weights)
        {
            //a point is composed by X -> number of days since the first weighing, Y -> value of the weighing in kgs
            var firstWeighingDate = weights.GetFirstWeightingDate();

            var points = new List<Point>();
            foreach (var weightHistory in weights)
            {
                var point = new Point
                {
                    X = (weightHistory.DateOfMeasurement - firstWeighingDate).TotalDays,
                    Y = weightHistory.WeightInKgs
                };

                points.Add(point);
            }

            return points;
        }
    }
}