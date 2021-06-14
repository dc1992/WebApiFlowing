using WebApiFlowing.DTOs;
using WebApiFlowing.DTOs.BusinessLogic;

namespace WebApiFlowing.BusinessLogic.Interfaces
{
    public interface IWeightTrendCalculator
    {
        Target EstimateTarget(User user);
    }
}