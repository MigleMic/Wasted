using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Wasted.API.Data;
using Wasted.API.Dtos;
using Wasted.API.Models;
using System.Collections.Generic;

namespace Wasted.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishRepo _repository;
        private readonly IProductRepo _productRepository;
        private readonly IIngredientRepo _ingredientRepository;
        private readonly IMapper _mapper;

        public DishController(IDishRepo repository, IProductRepo productRepository, IIngredientRepo ingredientRepository, IMapper mapper)
        {
            _repository = repository;
            _productRepository = productRepository;
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }

        //GET api/dish
        [HttpGet]
        public ActionResult <List<DishReadDto>> GetDishList()
        {
            List<DishWEB> finalDishList = new List<DishWEB>();
            var dishList = _repository.GetDishList();
            if (dishList != null)
            {
                foreach (var dish in dishList)
                {
                    DishWEB newDish = new DishWEB();
                    newDish.Id = dish.Id;
                    newDish.Name = dish.Name;
                    newDish.numberOfIngredients = dish.numberOfIngredients;
                    newDish.Type = dish.Type;
                    
                    var ingredients = _ingredientRepository.GetIngredientListByDishId(dish.Id);
                    List<IngredientWEB> dishIngredients = new List<IngredientWEB>();
                    foreach (var ingredient in ingredients)
                    {
                        IngredientWEB newItem = new IngredientWEB();
                        var product = _productRepository.GetProductById(ingredient.ProductId);
                        newItem.Item = product.Name;
                        newItem.Amount = ingredient.Amount;
                        newItem.Unit = product.MeasurementUnits;
                        dishIngredients.Add(newItem); 
                    }
                    newDish.Ingredients = dishIngredients;
                    finalDishList.Add(newDish);
                }
                return Ok(finalDishList);
            }
            return NotFound();
        }

        //GET api/dish/{id}
        [HttpGet("{id}", Name = "GetDishById")]
        public ActionResult <DishReadDto> GetDishById(int id)
        {
            var dishModelFromRepo = _repository.GetDishById(id);
            if (dishModelFromRepo != null)
            {
                return Ok(_mapper.Map<DishReadDto>(dishModelFromRepo));
            }
            return NotFound();
        }

        //POST api/dish
        [HttpPost]
        public ActionResult <DishReadDto> CreateNewDish(DishCreateDto dishCreate)
        {
            var dishModel = _mapper.Map<Dish>(dishCreate);

            _repository.CreateNewDish(dishModel);
            _repository.SaveChanges();

            var dishReadDto = _mapper.Map<DishReadDto>(dishModel);

            return CreatedAtRoute (nameof(GetDishById), new {Id = dishModel.Id}, dishReadDto);
        }

        //PUT api/dish/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateDish(int id, DishUpdateDto dishUpdateDto)
        {
            var dishModelFromRepo = _repository.GetDishById(id);
            if (dishModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(dishUpdateDto, dishModelFromRepo);

            _repository.UpdateDish(dishModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        //DELETE api/dish/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteDish(int id)
        {
            var dishModelFromRepo = _repository.GetDishById(id);
            if (dishModelFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteDish(dishModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
