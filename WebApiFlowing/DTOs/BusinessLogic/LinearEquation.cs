namespace WebApiFlowing.DTOs.BusinessLogic
{
    public class LinearEquation
    {
        public LinearEquation(double m, double b)
        {
            M = m;
            B = b;
        }

        public double M { get; set; }

        public double B { get; set; }

        public override string ToString()
        {
            return $"y = {M}x + {B}";
        }
    }
}