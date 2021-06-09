using System;

namespace WebApiFlowing.DTOs.Response
{
    public class WeightHistory
    {
        public DateTimeOffset DateOfMeasurement { get; set; }

        public double WeightInKgs { get; set; }
    }
}