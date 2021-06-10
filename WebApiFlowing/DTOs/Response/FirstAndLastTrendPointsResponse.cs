namespace WebApiFlowing.DTOs.Response
{
    public class FirstAndLastTrendPointsResponse
    {
        public WeightHistory FirstTrendPoint { get; set; }

        public WeightHistory LastTrendPoint { get; set; }
    }
}