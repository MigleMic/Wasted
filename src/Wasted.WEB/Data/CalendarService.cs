using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasted.Data;

namespace Wasted.Data
{
    public class CalendarService
    {
        private readonly JsonFileService _jsonFileService;

        public CalendarService(JsonFileService jsonFileService)
        {
            _jsonFileService = jsonFileService;
        }

        public List<CalendarItem> GetCalendarItems()
        {
            var calendarItems = new List<CalendarItem>();
            var path = "CalendarsItems.json";
            try
            {
                calendarItems = JsonConvert.DeserializeObject<List<CalendarItem>>(_jsonFileService.ReadJsonFromFile(path));
            }
            catch (Exception e)
            {
                Log.Error("Exception caught {0}", e);
            }
            return calendarItems;
        }

        public List<CalendarItem> GetCalendarItemsUser(int userId)
        {
            var usersCalendar = new List<CalendarItem>();
            List<CalendarItem> all = GetCalendarItems();
            foreach (var item in all)
            {
                if(item.UserId == userId)
                {
                    usersCalendar.Add(item);
                }
            }
            return usersCalendar;
        }

        public void AddCalendarItem(int userId, CalendarItem calendarItem, List<Product> edible, List<DishModel> dishes)
        {
            List<CalendarItem> allItems = GetCalendarItems();
            calendarItem.UserId = userId;
            foreach (var product in edible)
            {
                if(product.Id == calendarItem.ProductId)
                {
                    calendarItem.ProductName = product.Name;
                    calendarItem.EnergyValue = (int)product.EnergyValue;
                }
            }

            foreach (var dish in dishes)
            {
                if(dish.Id == calendarItem.ProductId)
                {
                    calendarItem.ProductName = dish.Name;
                    calendarItem.EnergyValue = 2000;
                }
            }
            if(calendarItem.Time == "1")
            {
                calendarItem.Time = "Breakfast";
            }
            if(calendarItem.Time == "2")
            {
                calendarItem.Time = "Lunch";
            }
            else
            {
                calendarItem.Time = "Dinner";
            }

            try
            {
                allItems.Add(calendarItem);
                writeToJson("CalendarsItems.json", allItems);
            }
            catch (Exception e)
            {
                Log.Error("Exception caught {0}", e);
            }
        }

        public void DeleteCalendarItem(int userId, int productId, string day, string time)
        {
            List<CalendarItem> allItems = GetCalendarItems();
      
            try 
            {
                allItems.RemoveAll(item => item.ProductId == productId && item.UserId == userId && item.Day == day && item.Time == time);
                writeToJson("CalendarsItems.json", allItems);
            }
            catch (Exception e)
            {
                Log.Error("Exception caught {0}", e);
            }
        }

        public void writeToJson(string file, List<CalendarItem> calendarItems)
        {
            try
            {
                Log.Information("Starting writing to CalendarItems.json");
                string json = JsonConvert.SerializeObject(calendarItems, Formatting.Indented);
                File.WriteAllText(file, json);
                Log.Information("Finished writing to CalendarItems.json");
            }
            catch (Exception e)
            {
                Log.Error("Exception caught when writing to CalendarItems {0}", e);
            }
        }

        

        public Task<List<CalendarItem>> SaveCalendarItems(List<CalendarItem> allItems)
        {
            var filePath = "CalendarsItems.json";
            try
            {
                Log.Information("Starting writing Calendar");
                _jsonFileService.WriteJsonToFile(JsonConvert.SerializeObject(allItems, Formatting.Indented), filePath);
                Log.Information("Finished writing Calendar");
            }
            catch (Exception e)
            {
                Log.Error("Exception caught: {0}", e);
            }
            return Task.FromResult(allItems);
        }
    }
}