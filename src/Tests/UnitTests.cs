using System;
using Xunit;
using Wasted.Data;
//using Wasted.API.Models;
using System.Collections.Generic;
using Xunit.Abstractions;

namespace Tests
{
    public class UnitTest
    {

        private readonly ITestOutputHelper output;

        public UnitTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        public bool IsVisible(CalendarItem Item)
        {
            return true;

        }

        [Fact]
        public void AdminApproveTest()
        {

            ProductService productService = new ProductService(new HttpHelper());

            Product newProduct = new Product
            {
                Name = "Test",
                Type = "Test",
                MeasurementUnits = "Test",
                EnergyValue = 123,
                AdminApproved = false
            };


            var id = productService.AddProduct(newProduct).Result;
            var allProducts = productService.GetAllProducts(250).Result;
            productService.ApproveProduct(allProducts, newProduct, id);

            var allProductsAfter = productService.GetAllProducts(250).Result;

            foreach (var product in allProductsAfter)
            {
                if (product.Id == id + 1)
                {
                    Assert.True(product.AdminApproved);
                    productService.DeleteProduct(product.Id);
                }

            }
        }

        [Fact]
        public void CalendarTest()
        {
            CalendarService calendarService = new CalendarService(new JsonFileService());
            ProductService productService = new ProductService(new HttpHelper());
            RecipeCalcService dishesService = new RecipeCalcService(new HttpHelper());

            var allProducts = productService.GetAllProducts(250).Result;
            var allDishes = dishesService.GetRecipes().Result;


            CalendarItem newCalendarItem = new CalendarItem
            {
                ProductName = "Test",
                ProductId = 27,
                Day = "1",
                Quantity = 2,
                EnergyValue = 123,
                Time = "2",
                UserId = 9
            };

            CalendarItem newCalendarItemAfter = new CalendarItem
            {
                ProductName = "Buttermilk",
                ProductId = 27,
                Day = "1",
                Quantity = 2,
                EnergyValue = 175,
                Time = "Lunch",
                UserId = 9
            };

            calendarService.AddCalendarItem(9, newCalendarItem, allProducts, allDishes);

            var calendarList = calendarService.GetCalendarItemsUser(9);

            CalendarItem addedCalendarItem;

            output.WriteLine(calendarList[0].ProductName);
            output.WriteLine(calendarList[0].ProductId.ToString());
            output.WriteLine(calendarList[0].Day);
            output.WriteLine(calendarList[0].Quantity.ToString());
            output.WriteLine(calendarList[0].EnergyValue.ToString());
            output.WriteLine(calendarList[0].Time);
            output.WriteLine(calendarList[0].UserId.ToString());

            addedCalendarItem = new CalendarItem
            {
                ProductName = calendarList[0].ProductName,
                ProductId = calendarList[0].ProductId,
                Day = calendarList[0].Day,
                Quantity = calendarList[0].Quantity,
                EnergyValue = calendarList[0].EnergyValue,
                Time = calendarList[0].Time,
                UserId = calendarList[0].UserId
            };

            Assert.Equal(newCalendarItemAfter, addedCalendarItem);

        }
    }
}
