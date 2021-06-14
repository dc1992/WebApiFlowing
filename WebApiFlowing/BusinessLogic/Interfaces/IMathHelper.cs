using System.Collections.Generic;
using WebApiFlowing.DTOs.BusinessLogic;

namespace WebApiFlowing.BusinessLogic.Interfaces
{
    public interface IMathHelper
    {
        IMathFunction CalculateLinearLeastSquares(ICollection<Point> points);
    }
}