using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiFlowing.BusinessLogic.Extensions;
using WebApiFlowing.Data.Interfaces;
using WebApiFlowing.DTOs.Response;
using WeightHistory = WebApiFlowing.DTOs.WeightHistory;

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

            var response = GetUserWeightsResponseFrom(user.WeightHistories);

            return response;
        }

        private UserWeightsResponse GetUserWeightsResponseFrom(ICollection<WeightHistory> weightHistories)
        {
            var orderedWeights = weightHistories.GetOrderedCollection();

            var response = new UserWeightsResponse
            {
                WeightHistories = orderedWeights
                    .Select(wh => new DTOs.Response.WeightHistory
                    {
                        DateOfMeasurement = wh.DateOfMeasurement,
                        WeightInKgs = wh.WeightInKgs
                    }).ToList()
            };

            return response;
        }
    }
}