using System;
using System.Collections.Generic;

namespace WebApiFlowing.DTOs
{
    public class User
    {
        public User()
        {
            WeightHistories = new List<WeightHistory>();
        }

        public Guid Guid { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public double DesiredWeightInKgs { get; set; }

        public double HeightInMeters { get; set; }

        public ICollection<WeightHistory> WeightHistories { get; set; }
    }
}