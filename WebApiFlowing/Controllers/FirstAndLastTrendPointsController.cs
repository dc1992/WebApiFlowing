using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApiFlowing.BusinessLogic.Extensions;
using WebApiFlowing.BusinessLogic.Interfaces;
using WebApiFlowing.Data.Interfaces;
using WebApiFlowing.DTOs.API.Response;
using WebApiFlowing.DTOs.API.Shared;

namespace WebApiFlowing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FirstAndLastTrendPointsController : ControllerBase
    {
        private IUserRepository _userRepository;
        private IWeightCalculator _weightCalculator;
        private IMathHelper _mathHelper;

        public FirstAndLastTrendPointsController(IUserRepository userRepository, IWeightCalculator weightCalculator, IMathHelper mathHelper)
        {
            _userRepository = userRepository;
            _weightCalculator = weightCalculator;
            _mathHelper = mathHelper;
        }

        [HttpGet]
        public async Task<FirstAndLastTrendPointsResponse> Get(Guid userGuid)
        {
            var user = await _userRepository.GetUserInfosBy(userGuid);
            user.ShouldNotBeNull();

            var trendLinearEquation = _weightCalculator.CalculateTrend(user);

            //find x and y of first trend point
            var firstWeighingDate = user.WeightHistories.GetFirstWeightingDate();
            var firstTrendPointY = _mathHelper.FindZero(trendLinearEquation);

            //find x and y of last trend point
            var daysFromStarting = _mathHelper.FindXByY(trendLinearEquation, user.DesiredWeightInKgs);
            var estimatedDate = firstWeighingDate.AddDays((int)daysFromStarting);

            if (estimatedDate < firstWeighingDate)
                throw new ArgumentOutOfRangeException("Estimated date is before first date");

            var response = new FirstAndLastTrendPointsResponse
            {
                FirstTrendPoint = new TrendPoint
                {
                    X = firstWeighingDate,
                    Y = firstTrendPointY
                },
                LastTrendPoint = new TrendPoint
                {
                    X = estimatedDate,
                    Y = user.DesiredWeightInKgs
                }
            };

            return response;
        }
    }
}