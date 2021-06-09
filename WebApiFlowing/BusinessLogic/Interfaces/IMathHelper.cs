using System.Collections.Generic;
using WebApiFlowing.DTOs.BusinessLogic;

namespace WebApiFlowing.BusinessLogic.Interfaces
{
    public interface IMathHelper
    {
        LinearEquation CalculateLinearLeastSquares(ICollection<Point> points);

        double FindXByY(LinearEquation linearEquation, double y);
    }
}