using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApiFlowing.BusinessLogic.Extensions;
using WebApiFlowing.BusinessLogic.Interfaces;
using WebApiFlowing.Data.Interfaces;
using WebApiFlowing.DTOs.API.Response;

namespace WebApiFlowing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EstimatedDateForReachingWeightController : ControllerBase
    {
        private IUserRepository _userRepository;
        private IWeightTrendCalculator _weightCalculator;

        public EstimatedDateForReachingWeightController(IUserRepository userRepository, IWeightTrendCalculator weightCalculator)
        {
            _userRepository = userRepository;
            _weightCalculator = weightCalculator;
        }

        [HttpGet]
        public async Task<EstimatedDateForReachingWeightResponse> Get(Guid userGuid)
        {
            var user = await _userRepository.GetUserInfosBy(userGuid);
            user.ShouldNotBeNull();

            var estimatedDate = _weightCalculator.EstimateTargetDate(user);

            var response = new EstimatedDateForReachingWeightResponse
            {
                EstimatedDate = estimatedDate,
                DesiredWeightInKgs = user.DesiredWeightInKgs
            };

            return response;
        }
    }
}