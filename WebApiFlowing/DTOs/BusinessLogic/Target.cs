using System;

namespace WebApiFlowing.DTOs.BusinessLogic
{
    public class Target
    {
        public DateTimeOffset EstimatedDate { get; set; }

        public LinearEquation Trend { get; set; }
    }
}