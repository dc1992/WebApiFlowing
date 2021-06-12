using System;

namespace WebApiFlowing.DTOs.API.Shared
{
    public class WeightHistory
    {
        public DateTimeOffset DateOfMeasurement { get; set; }

        public double WeightInKgs { get; set; }
    }
}