using System;
using Xunit;
using Wasted.Data;
using System.Collections.Generic;
using Xunit.Abstractions;

namespace Tests
{
    public class IntegrationTest
    {
        [Fact]
        public void CalendarItemDeleteTest() //Possible to delete items from calendar
        {
            CalendarService calendarService = new CalendarService(new JsonFileService());
            ProductService productService = new ProductService(new HttpHelper());
            RecipeCalcService dishesService = new RecipeCalcService(new HttpHelper());

            var allProducts = productService.GetAllProducts(250).Result;
            var allDishes = dishesService.GetRecipes().Result;

            CalendarItem newCalendarItem = new CalendarItem
            {
                ProductName = "Buttermilk",
                ProductId = 27,
                Day = "1",
                Quantity = 2,
                EnergyValue = 175,
                Time = "3",
                UserId = 9
            };

            calendarService.AddCalendarItem(10, newCalendarItem, allProducts, allDishes);

            var list = calendarService.GetCalendarItemsUser(10);

            Assert.Single(list);

            calendarService.DeleteCalendarItem(10, 27, "1", "Dinner");

            list = calendarService.GetCalendarItemsUser(10);

            Assert.Empty(list);
        }
    }
}
