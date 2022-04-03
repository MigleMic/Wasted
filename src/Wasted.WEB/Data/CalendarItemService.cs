﻿using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasted.Data
{
    public class CalendarItemService
    {
        private readonly JsonFileService _jsonFileService;

        public CalendarItemService(JsonFileService jsonFileService)
        {
            _jsonFileService = jsonFileService;
        }

        public List<CalendarItem> GetCalendarItems()
        {
            var calendarItems = new List<CalendarItem>();
            var path = "CalendarItems.json";
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

        public void ReadCalendarItem(List<CalendarItem> calendarItems)
        {
            System.IO.StreamReader CalendarFile = new System.IO.StreamReader(@"CalendarItems.txt");
            //calendarItems.Clear();
            do
            {
                int userId = Int32.Parse(CalendarFile.ReadLine());
                int productId = Int32.Parse(CalendarFile.ReadLine());
                string productName = CalendarFile.ReadLine();
                int quantity = Int32.Parse(CalendarFile.ReadLine());
                int energyValue = Int32.Parse(CalendarFile.ReadLine());
                string day = CalendarFile.ReadLine();
                int time = Int32.Parse(CalendarFile.ReadLine());
                calendarItems.Add(new CalendarItem() { UserId = userId, ProductId = productId, ProductName = productName, Quantity = quantity, EnergyValue = energyValue, Day = day, Time = time });
            } while (CalendarFile.ReadLine() != null);
        }

        public void AddCalendarItem(int userId, CalendarItem calendarItem)
        {
            List<CalendarItem> allItems = GetCalendarItems();
            try
            {
                allItems.Add(calendarItem);
                string json = JsonConvert.SerializeObject(allItems, Formatting.Indented);
                File.WriteAllText("CalendarItems.json", json);
            }
            catch (Exception e)
            {
                Log.Error("Exception caught {0}", e);
            }
        }

        public Task<List<CalendarItem>> SaveCalendarItems(List<CalendarItem> allItems)
        {
            var filePath = "CalendarItems.json";
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