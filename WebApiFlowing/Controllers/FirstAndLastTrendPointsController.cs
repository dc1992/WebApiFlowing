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
        private IWeightTrendCalculator _weightCalculator;
        private IMathHelper _mathHelper;

        public FirstAndLastTrendPointsController(IUserRepository userRepository, IWeightTrendCalculator weightCalculator, IMathHelper mathHelper)
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

            //check if result is reachable with current trend
            var lastWeighingDate = user.WeightHistories.GetLastWeightingDate();
            if (estimatedDate < lastWeighingDate)
                throw new ArgumentOutOfRangeException("Target not reacheble with current trend");

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