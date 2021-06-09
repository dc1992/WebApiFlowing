using System;

namespace WebApiFlowing.DTOs.Controllers
{
    public class EstimatedDateForReachingWeightResponse
    {
        public DateTimeOffset EstimatedDate { get; set; }

        public double DesiredWeightInKgs { get; set; }
    }
}