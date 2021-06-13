using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiFlowing.BusinessLogic.Extensions;
using WebApiFlowing.BusinessLogic.Interfaces;
using WebApiFlowing.Data.Interfaces;
using WebApiFlowing.DTOs;

namespace WebApiFlowing.BusinessLogic
{
    public class BodyMassIndexCalculator : IBodyMassIndexCalculator
    {
        private IDataContext _dataContext;

        public BodyMassIndexCalculator(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IdealWeightRange> GetIdealWeightRange(double heightInMeters)
        {
            var idealWeightRange = await _dataContext.BodyMassIndexRanges
                .AsNoTracking()
                .Where(idr => idr.IsIdeal)
                .Select(idr => new { idr.MinimumBMI, idr.MaximumBMI })
                .SingleOrDefaultAsync();

            idealWeightRange.ShouldNotBeNull();

            //since bmi = weight / height^2 --> weight = bmi * height^2
            var minimumWeight = idealWeightRange.MinimumBMI * Math.Pow(heightInMeters, 2);
            var maximumWeight = (idealWeightRange.MaximumBMI ?? double.MaxValue)  * Math.Pow(heightInMeters, 2);

            var idealRange = new IdealWeightRange
            {
                MinimumIdealWeightInKgs = minimumWeight,
                MaximumIdealWeightInKgs = maximumWeight
            };

            return idealRange;
        }
    }
}