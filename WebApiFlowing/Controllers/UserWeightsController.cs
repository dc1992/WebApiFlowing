using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApiFlowing.BusinessLogic.Extensions;
using WebApiFlowing.Data.Interfaces;
using WebApiFlowing.DTOs.Response;

namespace WebApiFlowing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserWeightsController : ControllerBase
    {
        private IUserRepository _userRepository;

        public UserWeightsController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<UserWeightsResponse> Get(Guid userGuid)
        {
            var response = new UserWeightsResponse
            {
            };

            return response;
        }
    }
}