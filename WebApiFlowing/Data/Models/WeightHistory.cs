using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiFlowing.Data.Models
{
    public class WeightHistory
    {
        [Key]
        public int Id { get; set; }

        public DateTimeOffset DateOfMeasurement { get; set; }

        [Range(20, 600)]
        public double WeightInKgs { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}