using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApiFlowing.BusinessLogic.Extensions;
using WebApiFlowing.Data.Interfaces;

namespace WebApiFlowing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserWeightTargetDateController : ControllerBase
    {
        private IUserRepository _userRepository;

        public UserWeightTargetDateController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<DateTimeOffset> Get(Guid userGuid)
        {
            var user = await _userRepository.GetUserInfosBy(userGuid);
            user.ShouldNotBeNull();

            return new DateTimeOffset();
        }
    }
}