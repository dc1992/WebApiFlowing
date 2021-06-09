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
            var user = await _userRepository.GetUserInfosBy(userGuid);
            user.ShouldNotBeNull();

            var response = new UserWeightsResponse
            {
                WeightHistories = user.WeightHistories
                    .OrderBy(wh => wh.DateOfMeasurement)
                    .Select(wh => new WeightHistory
                    {
                        DateOfMeasurement = wh.DateOfMeasurement,
                        WeightInKgs = wh.WeightInKgs
                    }).ToList()
            };

            return response;
        }
    }
}