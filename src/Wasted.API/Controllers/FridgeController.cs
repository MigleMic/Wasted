using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Wasted.API.Data;
using Wasted.API.Dtos;
using Wasted.API.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Wasted.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FridgeController : ControllerBase
    {
        private readonly IFridgeRepo _repository;
        private readonly IProductRepo _productRepository;
        private readonly IMapper _mapper;

        public FridgeController(IFridgeRepo repository, IProductRepo productRepository, IMapper mapper)
        {
            _repository = repository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        //GET api/product/{userId}
        [HttpGet("{userId}")]
        public ActionResult <IEnumerable<FridgeItemWEB>> GetFridgeItemList(int userId)
        {
            var fridgeItemList = _repository.GetFridgeItemList(userId);
            var products = _productRepository.GetProductList();
            var fridgeItemWEBList = new List<FridgeItemWEB>();
            if (fridgeItemList != null)
            {
                foreach (var item in fridgeItemList)
                {
                    var temp = new FridgeItemWEB();
                    var product = products.Where(p => p.Id == item.ProductId).FirstOrDefault();
                    temp.Name = product.Name;
                    temp.Type = product.Type;
                    temp.ProductId = item.ProductId;
                    temp.MeasurementUnits = product.MeasurementUnits;
                    temp.Amount = item.Amount;
                    temp.Date = item.Date;
                    fridgeItemWEBList.Add(temp);
                }
                
                return Ok(fridgeItemWEBList);
            }
            return NotFound();
        }
        //POST api/product/{userId}/
        [HttpPost("{userId}")]
        public ActionResult <FridgeItem> CreateNewFridgeItem(int userId, FridgeItemWEB fridgeItemCreate)
        {
            var fridgeItem = new FridgeItem();
            fridgeItem.ProductId = fridgeItemCreate.ProductId;
            fridgeItem.Amount = fridgeItemCreate.Amount;
            fridgeItem.Date = fridgeItemCreate.Date;
            fridgeItem.UserId = userId;
            _repository.CreateFridgeItem(fridgeItem);
            _repository.SaveChanges();

            return CreatedAtRoute (1,1);
        }

        //DELETE api/fridge/{userId}/{productId}
        [HttpDelete("{userId}/{productId}")]
        public ActionResult DeleteProduct(int userId, int productId)
        {
            var productModelFromRepo = _repository.GetFridgeItem(userId, productId);
            if (productModelFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteFridgeItem(productModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
