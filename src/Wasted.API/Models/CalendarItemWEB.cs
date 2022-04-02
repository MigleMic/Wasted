using System;
using System.ComponentModel.DataAnnotations;

namespace Wasted.API.Models
{
    public class CalendarItemWEB 
    {
      public string Name { get; set; }
      public int ProductId { get; set; }
      public string ProductType { get; set; }
      public int Quantity { get; set; }
      public float EnergyValue { get; set; }
      public int Day { get; set; }
      public int Time { get; set; }  
    }
}