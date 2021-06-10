using System;
using System.Collections.Generic;
using System.Linq;
using WebApiFlowing.DTOs;

namespace WebApiFlowing.BusinessLogic.Extensions
{
    public static class WeightHistoryExtensions
    {
        public static DateTimeOffset GetFirstWeightingDate(this ICollection<WeightHistory> orderedWeights)
        {
            return orderedWeights.First().DateOfMeasurement;
        }
    }
}