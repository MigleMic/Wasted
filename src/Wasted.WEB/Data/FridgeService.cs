using System;
using System.Threading.Tasks;
using Serilog;
using System.Collections.Generic;
using System.Linq;

namespace Wasted.Data
{
    public class FridgeService
    {

        private readonly HttpHelper _httpHelper;

        public FridgeService(HttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public async Task<List<FridgeItem>> GetFridgeItems(int userId)
        {
            List<FridgeItem> fridgeItems = null;
            try 
            {
                Log.Information("Starting to read FridgeItems");
                fridgeItems =  new List<FridgeItem>(await _httpHelper.GetList<FridgeItem>("fridge/"+ userId.ToString()));
                Log.Information("Finished reading FridgeItems");
                
            }
            catch (Exception e)
            {
                Log.Error("Exception caught: {0}",e);
            }
            return fridgeItems;
        }
        public async Task AddFridgeItem(FridgeItem fridgeItem, int userId)
        {
            try 
            {
                Log.Information("Starting to add fridge item: {0}", fridgeItem.Name);
                var id =  await _httpHelper.Post<FridgeItem>(fridgeItem,"fridge/" + userId.ToString());
                Log.Information("Finished adding fridge item: {0}", fridgeItem.Name);
            }
            catch (Exception e)
            {
                Log.Error("Exception caught: {0}",e);
            }
        }
        public void DeleteFridgeItem(int productId, int userId)
        {
            try 
            {
                Log.Information("Starting to delete product id: {0} from user id: {1} fridge", productId, userId);
                _httpHelper.Delete(productId,"fridge/"+ userId.ToString());
                Log.Information("Finished deleting fridge item");
                
            }
            catch (Exception e)
            {
                Log.Error("Exception caught: {0}",e);
            }
        }

        public async Task<List<FridgeItem>> GetFridgeItemForCalendar(int userId)
        {
            List<FridgeItem> items = await GetFridgeItems(userId);
            List<FridgeItem> calendarItems = new List<FridgeItem>();
            foreach(var products in items)
            {
                if (products.Type == "Berry" || products.Type == "Candy" || products.Type == "Drink" ||
                    products.Type == "Fruit" || products.Type == "Dish")
                {
                    calendarItems.Add(products);
                }
            }
            return calendarItems;
        }

    }
}
