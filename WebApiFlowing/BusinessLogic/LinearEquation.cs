using WebApiFlowing.BusinessLogic.Interfaces;

namespace WebApiFlowing.BusinessLogic
{
    public class LinearEquation : IMathFunction
    {
        public double M { get; }

        public double B { get; }

        public LinearEquation(double m, double b)
        {
            M = m;
            B = b;
        }


        public override string ToString()
        {
            return $"y = {M}x + {B}";
        }

        public double FindXByY(double y)
        {
            //since y = mx + b  -->  x = (y - b)/m
            var x = (y - B) / M;

            return x;
        }

        public double FindZero()
        {
            //we can ignore the mx since x is zero -> y = (linearEquation.M * 0) + b
            var y = B;

            return y;
        }
    }
}