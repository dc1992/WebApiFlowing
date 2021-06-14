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

            var estimatedTarget = _weightCalculator.EstimateTarget(user);

            //first point
            var firstWeighingDate = user.WeightHistories.GetFirstWeightingDate();
            var firstTrendPointY = _mathHelper.FindZero(estimatedTarget.Trend);

            //last point
            var estimatedDate = estimatedTarget.EstimatedDate;
            var desiredWeightInKgs = user.DesiredWeightInKgs;

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
                    Y = desiredWeightInKgs
                }
            };

            return response;
        }
    }
}