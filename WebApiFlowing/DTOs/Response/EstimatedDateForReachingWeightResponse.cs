using System;

namespace WebApiFlowing.DTOs.Response
{
    public class EstimatedDateForReachingWeightResponse
    {
        public DateTimeOffset EstimatedDate { get; set; }

        public double DesiredWeightInKgs { get; set; }
    }
}