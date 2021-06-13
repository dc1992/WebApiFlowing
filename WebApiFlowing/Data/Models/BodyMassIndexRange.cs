using System.ComponentModel.DataAnnotations;

namespace WebApiFlowing.Data.Models
{
    public class BodyMassIndexRange
    {
        [Key]
        public int Id { get; set; }
        
        [Range(0, double.MaxValue)]
        public double MinimumBMI { get; set; }

        [Range(0, double.MaxValue)]
        public double MaximumBMI { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public bool IsIdeal { get; set; }
    }
}