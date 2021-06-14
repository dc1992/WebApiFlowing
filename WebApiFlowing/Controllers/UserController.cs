using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApiFlowing.Data.Interfaces;
using WebApiFlowing.DTOs;
using WebApiFlowing.DTOs.API.Request;
using WebApiFlowing.DTOs.API.Response;
using WeightHistory = WebApiFlowing.DTOs.API.Shared.WeightHistory;

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
            var userToInsert = GetUserToInsert(request);
            var insertedUser = await _userRepository.InsertUser(userToInsert);

            var response = GetResponse(insertedUser);

            return response;
        }

        private User GetUserToInsert(UserRequest request)
        {
            var userToInsert = new User
            {
                Name = request.Name,
                Surname = request.Surname,
                DesiredWeightInKgs = request.DesiredWeightInKgs.Value,
                HeightInMeters = request.HeightInMeters.Value,
                WeightHistories = request.WeightHistories.Select(wh => new WebApiFlowing.DTOs.WeightHistory
                {
                    DateOfMeasurement = wh.DateOfMeasurement,
                    WeightInKgs = wh.WeightInKgs
                }).ToList()
            };

            return userToInsert;
        }

        private UserResponse GetResponse(User user)
        {
            var response = new UserResponse
            {
                Guid = user.Guid,
                Name = user.Name,
                Surname = user.Surname,
                DesiredWeightInKgs = user.DesiredWeightInKgs,
                HeightInMeters = user.HeightInMeters,
                WeightHistories = user.WeightHistories.Select(wh => new WeightHistory
                {
                    DateOfMeasurement = wh.DateOfMeasurement,
                    WeightInKgs = wh.WeightInKgs
                }).ToList()
            };

            return response;
        }
    }
}