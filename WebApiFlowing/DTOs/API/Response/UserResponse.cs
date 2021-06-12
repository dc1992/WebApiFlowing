using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiFlowing.DTOs.API.Response
{
    public class UserResponse
    {
        public UserResponse()
        {
            WeightHistories = new List<WeightHistory>();
        }

        public Guid Guid { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        public double DesiredWeightInKgs { get; set; }

        public double HeightInMeters { get; set; }

        public ICollection<WeightHistory> WeightHistories { get; set; }
    }
}