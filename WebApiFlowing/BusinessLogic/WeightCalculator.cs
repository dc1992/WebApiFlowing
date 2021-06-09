using System;
using System.Collections.Generic;
using System.Linq;
using WebApiFlowing.BusinessLogic.Extensions;
using WebApiFlowing.BusinessLogic.Interfaces;
using WebApiFlowing.DTOs;
using WebApiFlowing.DTOs.BusinessLogic;

namespace WebApiFlowing.BusinessLogic
{
    public class WeightCalculator : IWeightCalculator
    {
        private IMathHelper _mathHelper;
        private const int MinimumNumberForEstimation = 2;

        public WeightCalculator(IMathHelper mathHelper)
        {
            _mathHelper = mathHelper;
        }

        public DateTimeOffset EstimateTargetDate(User user)
        {
            user.WeightHistories.ShouldContainAtLeast(MinimumNumberForEstimation);

            var orderedWeights = user.WeightHistories
                .OrderBy(wh => wh.DateOfMeasurement)
                .ToList();

            var firstWeighingDate = orderedWeights.First().DateOfMeasurement;

            var points = TurnUserWeightsInPoints(orderedWeights, firstWeighingDate);

            var linearEquation = _mathHelper.CalculateLinearLeastSquares(points);

            //using the linear equation found, we can find the estimated day (X) starting from the desidered user weight (Y)
            var daysFromStarting = _mathHelper.FindXByY(linearEquation, user.DesiredWeightInKgs);

            var estimatedDate = firstWeighingDate.AddDays((int) daysFromStarting);

            return estimatedDate;
        }

        private ICollection<Point> TurnUserWeightsInPoints(ICollection<WeightHistory> weights, DateTimeOffset firstWeighingDate)
        {
            //a point is composed by X -> number of days since the first weighing, Y -> value of the weighing in kgs

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