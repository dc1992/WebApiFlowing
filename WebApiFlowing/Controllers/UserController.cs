using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApiFlowing.Data.Interfaces;
using WebApiFlowing.DTOs.API.Request;
using WebApiFlowing.DTOs.API.Response;

namespace WebApiFlowing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<UserResponse> Post(UserRequest request)
        {

            return new UserResponse();
        }
    }
}