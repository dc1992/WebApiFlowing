﻿using System;
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

        public DateTimeOffset EstimateTargetDate(User user)
        {
            var trend = CalculateTrend(user);

            //using the linear equation found, we can find the estimated day (X) starting from the desidered user weight (Y)
            var daysFromStarting = _mathHelper.FindXByY(trend, user.DesiredWeightInKgs);

            var firstWeighingDate = user.WeightHistories.GetFirstWeightingDate();
            var estimatedDate = firstWeighingDate.AddDays((int) daysFromStarting);

            var lastWeighingDate = user.WeightHistories.GetLastWeightingDate();

            if (estimatedDate < lastWeighingDate)
                throw new ArgumentOutOfRangeException("Target not reacheble with current trend");

            return estimatedDate;
        }

        public LinearEquation CalculateTrend(User user)
        {
            user.WeightHistories.ShouldContainAtLeast(MinimumNumberForEstimation);

            var points = user.WeightHistories.ToPoints();

            var linearEquation = _mathHelper.CalculateLinearLeastSquares(points);

            return linearEquation;
        }
    }
}