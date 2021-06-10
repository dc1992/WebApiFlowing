using System;

namespace WebApiFlowing.DTOs.Response
{
    public class TrendPoint
    {
        public DateTimeOffset X { get; set; }

        public double Y { get; set; }
    }
}