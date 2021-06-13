using System.Threading.Tasks;
using WebApiFlowing.DTOs;

namespace WebApiFlowing.BusinessLogic.Interfaces
{
    public interface IBodyMassIndexCalculator
    {
        Task<IdealWeightRange> GetIdealWeightRange(double heightInMeters);
    }
}