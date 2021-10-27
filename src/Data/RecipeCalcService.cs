using System;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Serilog;

namespace Wasted.Data
{
    public class RecipeCalcService
    {
        private JsonFileService _jsonFileService;

        public RecipeCalcService(JsonFileService jsonFileService)
        {
            _jsonFileService = jsonFileService;
        }
        public List<RecipeItemModel> GetProducts()
        {
            var products =  new List<RecipeItemModel>();
            var filepath =  "RecipeProductList.json";
            try 
            {
                Log.Information("Started reading RecipeProductList");
                products = JsonConvert.DeserializeObject<List<RecipeItemModel>>(_jsonFileService.ReadJsonFromFile(filepath));
                Log.Information("Finished reading RecipeProductList");
            }
            catch (Exception e)
            {
                Log.Error("Exception caught: {0}",e);
            }
            return products;
        }
        public Task<List<RecipeItemModel>> SaveProducts(List<RecipeItemModel> products)
        {
            var filePath = "RecipeProductList.json";
            foreach(var product in products)
            {
                switch (product.Unit)
                {
                    case "kg":
                        product.Amount = product.Amount * 1000;
                        product.Unit = "g";
                        break;
                    case "l":
                        product.Amount = product.Amount * 1000;
                        product.Unit = "ml";
                        break;
                    case "oz":
                        product.Amount = product.Amount * 28;
                        product.Unit = "ml/g";
                        break;
                    default:
                        break;
                }
            } 
            try 
            {
                Log.Information("Starting writing RecipeProductList");
                _jsonFileService.WriteJsonToFile(JsonConvert.SerializeObject(products, Formatting.Indented),filePath);
                Log.Information("Finished writing RecipeProductlist");
            }
            catch (Exception e)
            {
                Log.Error("Exception caught: {0}",e);
            }
            return Task.FromResult(products);
        }

        public List<String> FindExpiringProducts(List<RecipeItemModel> products)
        {
            List<String> badProducts = new();
            try 
            {
                badProducts = products.Where(product => (DateTime.Parse(product.Date) - DateTime.Today).TotalDays <= 4 && (DateTime.Parse(product.Date) - DateTime.Today).TotalDays >= 0).Select(product => char.ToUpper(product.Item[0]) + product.Item.Substring(1)).ToList();
                Log.Information("Found all expiring products");
            }
            catch (Exception e)
            {
                Log.Error("Exception caught: {0}",e);
            }
            return badProducts;
        }

        public List<String> FindExpiredProducts(List<RecipeItemModel> products)
        {
            List<String> badProducts = new();
            try 
            {
                badProducts = products.Where(product => (DateTime.Parse(product.Date) - DateTime.Today).TotalDays < 0).Select(product => char.ToUpper(product.Item[0]) + product.Item.Substring(1)).ToList();
                Log.Information("Found all expired products");
            }
            catch (Exception e)
            {
                Log.Error("Exception caught: {0}",e);
            }
            return badProducts;
        }

        public List<DishModel> FindRecipe(List<RecipeItemModel> products)
        {
            List<DishModel> recipes = new List<DishModel>();
            List<DishModel> dishes = new List<DishModel>();
            try 
            {
                Log.Information("Starting to search for Recipes");
                var filepath = "Recipes.json";
                recipes = JsonConvert.DeserializeObject<List<DishModel>>(_jsonFileService.ReadJsonFromFile(filepath));
                int have;
                foreach (var recipe in recipes)
                {
                    have = 0;
                    for(int i = 0; i < recipe.numberOfIngredients; i++)
                    foreach (var product in products)
                    {
                        if(recipe.Ingredients[i].Item == product.Item.ToLower() && recipe.Ingredients[i].Amount <= product.Amount)
                        {
                            have++;
                        }
                    }
                    if(have == recipe.numberOfIngredients)
                    {
                        recipe.Relevance = recipe.numberOfIngredients;
                        dishes.Add(recipe);
                    }
                }
                Log.Information("Finished finding all recipes");
            }
            catch (Exception e)
            {
                Log.Error("Exception caught: {0}",e);
            }
            dishes.Sort();
            return dishes;
        }
    }
    // public class DishType
    // {
    //     public static string ReturnDishType(string sender)
    //     {
    //         string dishType = sender;
    //         // switch (sender)
    //         // {
    //         //     case "All":
    //         //         dishType = "All";
    //         //         break;
    //         //     case "Baked":
    //         //         dishType = "Baked";
    //         //         break;
    //         //     case "Pasta":
    //         //         dishType = "Pasta";
    //         //         break;
    //         //     case "Salad":
    //         //         dishType = "Salad";
    //         //         break;
    //         //     case "Soup":
    //         //         dishType = "Soup";
    //         //         break;
    //         //     default:
    //         //         dishType = "All";
    //         //         break;
    //         // }
    //         return dishType;
    //     }
    // }
}
