using System.Collections.Generic;

namespace WebApiFlowing.DTOs.API.Response
{
    public class UserWeightsResponse
    {
        public UserWeightsResponse()
        {
            WeightHistories = new List<Shared.WeightHistory>();
        }

        public ICollection<Shared.WeightHistory> WeightHistories { get; set; }
    }
}