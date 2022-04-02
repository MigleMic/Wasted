using System;
using System.Collections.Generic;
using System.Linq;
using Wasted.API.Data;
using Wasted.API.Models;

namespace Wasted.API.Data
{
    public class SqlCalendarRepo : ICalendarRepo
    {
        private readonly WastedContext _context;

        public SqlCalendarRepo(WastedContext context)
        {
            _context = context;
        }

        public void CreateCalendarItem(CalendarItem calendarItem)
        {
            if(calendarItem == null)
            {
                throw new ArgumentException(nameof(calendarItem));
            }

            _context.CalendarItems.Add(calendarItem);
        }

        public void DeleteCalendarItem(CalendarItem calendarItem)
        {
            if(calendarItem == null)
            {
                throw new ArgumentException(nameof(calendarItem));
            }

            _context.CalendarItems.Remove(calendarItem);
        }

        public CalendarItem GetCalendarItem(int userId, int productId)
        {
            return _context.CalendarItems.FirstOrDefault(p => p.ProductId == productId && p.UserId == userId);
        }

        public IEnumerable<CalendarItem> GetCalendarItemList(int userId)
        {
            return _context.CalendarItems.Where(item => item.UserId == userId).ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}