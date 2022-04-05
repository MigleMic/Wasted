using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Wasted.Data
{
    public struct CalendarItem
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public float EnergyValue { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
    }
}