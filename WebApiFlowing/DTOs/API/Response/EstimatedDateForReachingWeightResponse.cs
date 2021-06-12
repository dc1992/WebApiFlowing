using System;

namespace WebApiFlowing.DTOs.API.Response
{
    public class EstimatedDateForReachingWeightResponse
    {
        public DateTimeOffset EstimatedDate { get; set; }

        public double DesiredWeightInKgs { get; set; }
    }
}