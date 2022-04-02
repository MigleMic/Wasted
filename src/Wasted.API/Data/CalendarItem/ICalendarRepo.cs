using System.Collections.Generic;
using Wasted.API.Models;

namespace Wasted.API.Data
{
    public interface ICalendarRepo 
    {
        bool SaveChanges();
        IEnumerable<CalendarItem> GetCalendarItemList(int userId);
        CalendarItem GetCalendarItem(int userId, int productId);
        void CreateCalendarItem(CalendarItem calendarItem);
        void DeleteCalendarItem(CalendarItem calendarItem);
    }
}
