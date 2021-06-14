using System;
using WebApiFlowing.BusinessLogic.Extensions;
using WebApiFlowing.BusinessLogic.Interfaces;
using WebApiFlowing.DTOs;
using WebApiFlowing.DTOs.BusinessLogic;

namespace WebApiFlowing.BusinessLogic
{
    public class WeightTrendCalculator : IWeightTrendCalculator
    {
        private IMathHelper _mathHelper;
        private const int MinimumNumberForEstimation = 2;

        public WeightTrendCalculator(IMathHelper mathHelper)
        {
            _mathHelper = mathHelper;
        }

        public Target EstimateTarget(User user)
        {
            var trend = CalculateTrend(user);

            //using the linear equation found, we can find the estimated day (X) starting from the desidered user weight (Y)
            var daysFromStarting = trend.FindXByY(user.DesiredWeightInKgs);

            var firstWeighingDate = user.WeightHistories.GetFirstWeightingDate();
            var estimatedDate = firstWeighingDate.AddDays((int) daysFromStarting);

            //check if result is reachable with current trend
            var lastWeighingDate = user.WeightHistories.GetLastWeightingDate();
            if (estimatedDate < lastWeighingDate)
                throw new ArgumentOutOfRangeException("Target not reachable with current trend");

            var target = new Target
            {
                EstimatedDate = estimatedDate,
                Trend = trend
            };

            return target;
        }

        private IMathFunction CalculateTrend(User user)
        {
            user.WeightHistories.ShouldContainAtLeast(MinimumNumberForEstimation);

            var points = user.WeightHistories.ToPoints();

            var linearEquation = _mathHelper.CalculateLinearLeastSquares(points);

            return linearEquation;
        }
    }
}