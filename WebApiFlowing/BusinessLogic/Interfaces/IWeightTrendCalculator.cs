using System;
using WebApiFlowing.DTOs;
using WebApiFlowing.DTOs.BusinessLogic;

namespace WebApiFlowing.BusinessLogic.Interfaces
{
    public interface IWeightTrendCalculator
    {
        DateTimeOffset EstimateTargetDate(User user);

        LinearEquation CalculateTrend(User user);
    }
}