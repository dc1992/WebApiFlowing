using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApiFlowing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserWeightTargetDateController : ControllerBase
    {
        public UserWeightTargetDateController()
        {
            
        }

        [HttpGet]
        public async Task<DateTimeOffset> Get(Guid userGuid)
        {
            return new DateTimeOffset();
        }
    }
}