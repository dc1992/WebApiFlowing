using System;
using System.Threading.Tasks;
using WebApiFlowing.DTOs;

namespace WebApiFlowing.BusinessLogic.Interfaces
{
    public interface IWeightCalculator
    {
        DateTimeOffset EstimateTargetDate(User user);
    }
}