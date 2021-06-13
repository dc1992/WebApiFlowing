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
    public class IdealWeightController : ControllerBase
    {
        private IUserRepository _userRepository;
        private IBodyMassIndexCalculator _bodyMassIndexCalculator;
        private const int NumberOfDigitsAfterCommaInWeight = 2;

        public IdealWeightController(IUserRepository userRepository, IBodyMassIndexCalculator bodyMassIndexCalculator)
        {
            _userRepository = userRepository;
            _bodyMassIndexCalculator = bodyMassIndexCalculator;
        }

        [HttpGet]
        public async Task<IdealWeightResponse> Get(Guid userGuid)
        {
            var user = await _userRepository.GetUserInfosBy(userGuid);
            user.ShouldNotBeNull();

            var idealWeightRange = await _bodyMassIndexCalculator.GetIdealWeightRange(user.HeightInMeters);

            return new IdealWeightResponse
            {
                MaximumWeightInKgs = Math.Round(idealWeightRange.MaximumIdealWeightInKgs, NumberOfDigitsAfterCommaInWeight),
                MinimumWeightInKgs = Math.Round(idealWeightRange.MinimumIdealWeightInKgs, NumberOfDigitsAfterCommaInWeight)
            };
        }
    }
}