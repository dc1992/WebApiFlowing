using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiFlowing.DTOs.API.Request
{
    public class UserRequest
    {
        public UserRequest()
        {
            WeightHistories = new List<Shared.WeightHistory>();
        }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required]
        [Range(20, 600)]
        public double? DesiredWeightInKgs { get; set; }

        [Required]
        [Range(1, 3)]
        public double? HeightInMeters { get; set; }

        public ICollection<Shared.WeightHistory> WeightHistories { get; set; }
    }
}