using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApiFlowing.BusinessLogic.Extensions;
using WebApiFlowing.BusinessLogic.Interfaces;
using WebApiFlowing.Data.Interfaces;

namespace WebApiFlowing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EstimatedDateForReachingWeightController : ControllerBase
    {
        private IUserRepository _userRepository;
        private IWeightCalculator _weightCalculator;

        public EstimatedDateForReachingWeightController(IUserRepository userRepository, IWeightCalculator weightCalculator)
        {
            _userRepository = userRepository;
            _weightCalculator = weightCalculator;
        }

        [HttpGet]
        public async Task<DateTimeOffset> Get(Guid userGuid)
        {
            var user = await _userRepository.GetUserInfosBy(userGuid);
            user.ShouldNotBeNull();

            var estimatedDate = _weightCalculator.EstimateTargetDate(user);

            return estimatedDate;
        }
    }
}