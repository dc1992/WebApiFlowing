using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApiFlowing.BusinessLogic.Extensions;
using WebApiFlowing.BusinessLogic.Interfaces;
using WebApiFlowing.Data.Interfaces;
using WebApiFlowing.DTOs;
using WebApiFlowing.DTOs.BusinessLogic;
using WebApiFlowing.DTOs.Response;
using WeightHistory = WebApiFlowing.DTOs.Response.WeightHistory;

namespace WebApiFlowing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FirstAndLastTrendPointsController : ControllerBase
    {
        private IUserRepository _userRepository;
        private IMathHelper _mathHelper;

        public FirstAndLastTrendPointsController(IUserRepository userRepository, IMathHelper mathHelper)
        {
            _userRepository = userRepository;
            _mathHelper = mathHelper;
        }

        [HttpGet]
        public async Task<FirstAndLastTrendPointsResponse> Get(Guid userGuid)
        {
            var user = await _userRepository.GetUserInfosBy(userGuid);
            user.ShouldNotBeNull();

            //calculating linear equation to find the first and last trend points
            var orderedWeights = user.WeightHistories
                .OrderBy(wh => wh.DateOfMeasurement)
                .ToList();

            var points = orderedWeights.ToPoints();

            var trendLinearEquation = _mathHelper.CalculateLinearLeastSquares(points);


            //find x and y of first trend point
            var firstWeighingDate = orderedWeights.GetFirstWeightingDate();
            var firstTrendPointY = _mathHelper.FindZero(trendLinearEquation);

            //find x and y of last trend point
            var daysFromStarting = _mathHelper.FindXByY(trendLinearEquation, user.DesiredWeightInKgs);
            var estimatedDate = firstWeighingDate.AddDays((int)daysFromStarting);


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