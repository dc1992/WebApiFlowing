using System;
using WebApiFlowing.BusinessLogic.Interfaces;

namespace WebApiFlowing.DTOs.BusinessLogic
{
    public class Target
    {
        public DateTimeOffset EstimatedDate { get; set; }

        public IMathFunction Trend { get; set; }
    }
}