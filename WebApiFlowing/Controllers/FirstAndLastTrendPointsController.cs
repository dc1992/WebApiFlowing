using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApiFlowing.BusinessLogic.Extensions;
using WebApiFlowing.Data.Interfaces;
using WebApiFlowing.DTOs;
using WebApiFlowing.DTOs.Response;

namespace WebApiFlowing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FirstAndLastTrendPointsController : ControllerBase
    {
        private IUserRepository _userRepository;

        public FirstAndLastTrendPointsController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<FirstAndLastTrendPointsResponse> Get(Guid userGuid)
        {
            var user = await _userRepository.GetUserInfosBy(userGuid);
            user.ShouldNotBeNull();

            return new FirstAndLastTrendPointsResponse();
        }
    }
}