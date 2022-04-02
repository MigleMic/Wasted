using System;
using System.ComponentModel.DataAnnotations;

namespace Wasted.API.Models
{
    public class CalendarItem 
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public int ProductId { get; set; }
        
        [Required]
        public string ProductType { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public float EnergyValue { get; set; }
        
        [Required]
        public int Day { get; set; }

        [Required]
        public int Time { get; set; }

    }
}