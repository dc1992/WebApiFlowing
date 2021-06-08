using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiFlowing.Data.Models
{
    public class User
    {
        public User()
        {
            WeightHistories = new List<WeightHistory>();
        }

        [Key]
        public int Id { get; set; }

        public Guid Guid { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        [Range(20, 600)]
        public double DesiredWeightInKgs { get; set; }

        [Range(1, 3)]
        public double HeightInMeters { get; set; }

        public virtual ICollection<WeightHistory> WeightHistories { get; set; }
    }
}