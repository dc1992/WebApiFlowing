using System.Threading.Tasks;
using WebApiFlowing.DTOs.BusinessLogic;

namespace WebApiFlowing.BusinessLogic.Interfaces
{
    public interface IBodyMassIndexCalculator
    {
        Task<IdealWeightRange> GetIdealWeightRange(double heightInMeters);
    }
}