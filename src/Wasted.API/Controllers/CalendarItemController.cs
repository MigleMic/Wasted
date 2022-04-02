using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Wasted.API.Data;
using Wasted.API.Dtos;

using Wasted.API.Models;

namespace Wasted.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarItemController : ControllerBase
    {
        private readonly ICalendarRepo _repository;
        private readonly IProductRepo _productRepository;
        private readonly IDishRepo _dishRepository;
        private readonly IMapper _mapper;

        public CalendarItemController(ICalendarRepo repository, IProductRepo productRepository, IDishRepo dishRepository, IMapper mapper)
        {
            _repository = repository;
            _productRepository = productRepository;
            _dishRepository = dishRepository;
            _mapper = mapper;
        }

        //GET api/calendarItem{userId}
        [HttpGet("{userId}")]
        public ActionResult <IEnumerable<CalendarItemWEB>> GetCalendarItemList(int userId)
        {
            var calendarItems = _repository.GetCalendarItemList(userId);
            var products = _productRepository.GetProductList();
            var dishes = _dishRepository.GetDishList();
            var calendarItemWEBList = new List<CalendarItemWEB>();

            if(calendarItems != null)
            {
                foreach(var item in calendarItems)
                {
                    var temp = new CalendarItemWEB();
                    if (item.ProductType == "Product")
                    {
                        var product = products.Where(p => p.Id == item.ProductId).FirstOrDefault();
                        temp.Name = product.Name;
                        temp.ProductId = product.Id;
                        temp.ProductType = item.ProductType;
                        temp.Quantity = item.Quantity;
                        temp.EnergyValue = (float)product.EnergyValue;
                        temp.Day = item.Day;
                        temp.Time = item.Time;
                        calendarItemWEBList.Add(temp);
                    }
                    if(item.ProductType == "Dish")
                    {
                        var recipe = dishes.Where(r => r.Id == item.ProductId).FirstOrDefault();
                        temp.Name = recipe.Name;
                        temp.ProductId = recipe.Id;
                        temp.ProductType = item.ProductType;
                        temp.Quantity = item.Quantity;
                        temp.EnergyValue = 2000;
                        temp.Day = item.Day;
                        temp.Time = item.Time;
                        calendarItemWEBList.Add(temp);
                    }
                }
                return Ok(calendarItemWEBList);
            }
            return NotFound();
        }

        //POST api/calendarItem/{userId}/
        [HttpPost("{userId}")]
        public ActionResult <CalendarItem> CreateNewCalendarItem(int userId, CalendarItemWEB calendarItem)
        {
            var calItem = new CalendarItem(); 
            calItem.UserId = userId;
            calItem.ProductId = calendarItem.ProductId;
            calItem.ProductType = calendarItem.ProductType;
            calItem.Quantity = calendarItem.Quantity;
            calItem.EnergyValue = calendarItem.EnergyValue;
            calItem.Day = calendarItem.Day;
            calItem.Time = calendarItem.Time;
            _repository.SaveChanges();
            return CreatedAtRoute(1, 1);
            
        }

        //DELETE api/calendarItem
        [HttpDelete("{userId}/{productId}")] 
        public ActionResult DeleteCalendarItem (int userId, int productId)
        {
            var product = _repository.GetCalendarItem(userId, productId);
            if(product == null)
            {
                return NotFound();
            }

            _repository.DeleteCalendarItem(product);
            _repository.SaveChanges();

            return NoContent();

        }

    }
}