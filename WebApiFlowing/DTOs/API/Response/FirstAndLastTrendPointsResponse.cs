using WebApiFlowing.DTOs.API.Shared;

namespace WebApiFlowing.DTOs.API.Response
{
    public class FirstAndLastTrendPointsResponse
    {
        public TrendPoint FirstTrendPoint { get; set; }

        public TrendPoint LastTrendPoint { get; set; }
    }
}