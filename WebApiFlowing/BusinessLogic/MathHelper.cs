using System;
using System.Collections.Generic;
using WebApiFlowing.BusinessLogic.Interfaces;
using WebApiFlowing.DTOs;

namespace WebApiFlowing.BusinessLogic
{
    public class MathHelper : IMathHelper
    {
        public LinearEquation CalculateLinearLeastSquares(ICollection<Point> points)
        {
            throw new NotImplementedException();
        }

        public double FindXByY(LinearEquation linearEquation, double x)
        {
            throw new NotImplementedException();
        }
    }
}