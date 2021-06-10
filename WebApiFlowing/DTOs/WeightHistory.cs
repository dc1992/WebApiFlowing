using System;

namespace WebApiFlowing.DTOs
{
    public class WeightHistory
    {
        public DateTimeOffset DateOfMeasurement { get; set; }

        public double WeightInKgs { get; set; }
    }
}