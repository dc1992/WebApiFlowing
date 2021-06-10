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

            var points = orderedWeights.ToPoints();

            var linearEquation = _mathHelper.CalculateLinearLeastSquares(points);

            //using the linear equation found, we can find the estimated day (X) starting from the desidered user weight (Y)
            var daysFromStarting = _mathHelper.FindXByY(linearEquation, user.DesiredWeightInKgs);

            var firstWeighingDate = orderedWeights.GetFirstWeightingDate();
            var estimatedDate = firstWeighingDate.AddDays((int) daysFromStarting);

            return estimatedDate;
        }
    }
}