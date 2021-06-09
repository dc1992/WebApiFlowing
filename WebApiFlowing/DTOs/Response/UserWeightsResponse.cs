using System.Collections.Generic;

namespace WebApiFlowing.DTOs.Response
{
    public class UserWeightsResponse
    {
        public UserWeightsResponse()
        {
            WeightHistories = new List<WeightHistory>();
        }

        public ICollection<WeightHistory> WeightHistories { get; set; }
    }
}