using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiFlowing.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public Guid Guid { get; set; }
    }
}