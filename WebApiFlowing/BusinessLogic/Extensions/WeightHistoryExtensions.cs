using System;
using System.Collections.Generic;
using System.Linq;
using WebApiFlowing.DTOs;

namespace WebApiFlowing.BusinessLogic.Extensions
{
    public static class WeightHistoryExtensions
    {
        public static DateTimeOffset GetFirstWeightingDate(this ICollection<WeightHistory> weights)
        {
            weights.ShouldContainAtLeast(1);

            var orderedWeights = weights.GetOrderedCollection();

            return orderedWeights.First().DateOfMeasurement;
        }

        public static DateTimeOffset GetLastWeightingDate(this ICollection<WeightHistory> weights)
        {
            weights.ShouldContainAtLeast(1);

            var orderedWeights = weights.GetOrderedCollection();

            return orderedWeights.Last().DateOfMeasurement;
        }

        public static ICollection<WeightHistory> GetOrderedCollection(this ICollection<WeightHistory> source)
        {
            var orderedCollection = source
                .OrderBy(wh => wh.DateOfMeasurement)
                .ToList();

            return orderedCollection;
        }
    }
}