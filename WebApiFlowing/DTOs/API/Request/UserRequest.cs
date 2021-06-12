using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiFlowing.DTOs.API.Request
{
    public class UserRequest
    {
        public UserRequest()
        {
            WeightHistories = new List<WeightHistory>();
        }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        [Range(20, 600)]
        public double DesiredWeightInKgs { get; set; }

        [Range(1, 3)]
        public double HeightInMeters { get; set; }

        public ICollection<WeightHistory> WeightHistories { get; set; }
    }
}