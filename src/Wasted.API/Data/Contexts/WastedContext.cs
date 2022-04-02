using Microsoft.EntityFrameworkCore;
using Wasted.API.Models;

namespace Wasted.API.Data
{
    public class WastedContext : DbContext
    {
        public WastedContext(DbContextOptions<WastedContext> opt) : base(opt)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tip> Tips { get; set; }
        public DbSet<FridgeItem> FridgeItems { get; set; }
        public DbSet<CalendarItem> CalendarItems { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Apples", Type = "Fruit", MeasurementUnits = "kg", EnergyValue = 158.487},
                new Product { Id = 2, Name = "Troat", Type = "Fish", MeasurementUnits = "kg", EnergyValue = 284.546},
                new Product { Id = 3, Name = "Orange", Type = "Fruit", MeasurementUnits = "g", EnergyValue = 120.692},
                new Product { Id = 4, Name = "Blackberry", Type = "Berry", MeasurementUnits = "kg", EnergyValue = 262.178},
                new Product { Id = 5, Name = "Cheese", Type = "Dairy", MeasurementUnits = "kg", EnergyValue = 352.698},
                new Product { Id = 6, Name = "Bass", Type = "Fish", MeasurementUnits = "kg", EnergyValue = 271.745},
                new Product { Id = 7, Name = "Buttermilk", Type = "Dairy", MeasurementUnits = "l", EnergyValue = 175.1245},

                new Product { Id = 8, Name = "Flour", Type = "Grain", MeasurementUnits = "g", EnergyValue = 175.1245},
                new Product { Id = 9, Name = "Sugar", Type = "idk", MeasurementUnits = "g", EnergyValue = 175.1245},
                new Product { Id = 10, Name = "Milk", Type = "Dairy", MeasurementUnits = "ml", EnergyValue = 175.1245},
                new Product { Id = 11, Name = "Eggs", Type = "Dairy", MeasurementUnits = "unit(s)", EnergyValue = 175.1245},
                new Product { Id = 12, Name = "Butter", Type = "Dairy", MeasurementUnits = "g", EnergyValue = 175.1245},
                new Product { Id = 13, Name = "Salt", Type = "Spice", MeasurementUnits = "g", EnergyValue = 175.1245},
                new Product { Id = 14, Name = "Vanilla extract", Type = "Spice", MeasurementUnits = "g", EnergyValue = 175.1245},
                new Product { Id = 15, Name = "Cocoa", Type = "idk", MeasurementUnits = "g", EnergyValue = 175.1245},
                new Product { Id = 16, Name = "Dark Chocolate", Type = "idk", MeasurementUnits = "g", EnergyValue = 175.1245},
                new Product { Id = 17, Name = "Cinnamon", Type = "Spice", MeasurementUnits = "g", EnergyValue = 175.1245}
                );

            modelBuilder.Entity<Dish>().HasData(
                new Dish { Id = 1, Name =  "Pancakes", numberOfIngredients = 5, Type =  "Baked"},
                new Dish { Id = 2, Name = "Chocolate Cake", numberOfIngredients = 4, Type = "Baked"},
                new Dish { Id = 3, Name = "Waffles", numberOfIngredients = 7, Type = "All"},
                new Dish { Id = 4, Name = "Brownies", numberOfIngredients = 6, Type = "Baked"},
                new Dish { Id = 5, Name = "Apple Pie", numberOfIngredients = 6, Type = "Baked"}
              );

            modelBuilder.Entity<Ingredient>().HasKey(c => new { c.DishId, c.ProductId });

            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient { DishId = 1, ProductId =  8, Amount =  300 },
                new Ingredient { DishId = 1, ProductId =  9, Amount =  30 },
                new Ingredient { DishId = 1, ProductId =  10, Amount =  250 },
                new Ingredient { DishId = 1, ProductId =  11, Amount =  1 },
                new Ingredient { DishId = 1, ProductId =  12, Amount =  50 },

                new Ingredient { DishId = 2, ProductId =  8, Amount =  230 },
                new Ingredient { DishId = 2, ProductId =  15, Amount =  75 },
                new Ingredient { DishId = 2, ProductId =  9, Amount =  410 },
                new Ingredient { DishId = 2, ProductId =  11, Amount =  2 },

                new Ingredient { DishId = 3, ProductId =  8, Amount =  400 },
                new Ingredient { DishId = 3, ProductId =  13, Amount =  5 },
                new Ingredient { DishId = 3, ProductId =  9, Amount =  20 },
                new Ingredient { DishId = 3, ProductId =  11, Amount =  2 },
                new Ingredient { DishId = 3, ProductId =  10, Amount =  300 },
                new Ingredient { DishId = 3, ProductId =  12, Amount =  100 },
                new Ingredient { DishId = 3, ProductId =  14, Amount =  5 },

                new Ingredient { DishId = 4, ProductId =  8, Amount =  150 },
                new Ingredient { DishId = 4, ProductId =  12, Amount =  180 },
                new Ingredient { DishId = 4, ProductId =  15, Amount =  30 },
                new Ingredient { DishId = 4, ProductId =  9, Amount =  150 },
                new Ingredient { DishId = 4, ProductId =  11, Amount =  3 },
                new Ingredient { DishId = 4, ProductId =  16, Amount =  200 },

                new Ingredient { DishId = 5, ProductId =  1, Amount =  2 },
                new Ingredient { DishId = 5, ProductId =  17, Amount =  15 },
                new Ingredient { DishId = 5, ProductId =  12, Amount =  100 },
                new Ingredient { DishId = 5, ProductId =  8, Amount =  100 },
                new Ingredient { DishId = 5, ProductId =  9, Amount =  5 },
                new Ingredient { DishId = 5, ProductId =  11, Amount =  1 }
              );



            modelBuilder.Entity<Tip>().HasData(
                new Tip { TipId = 1, TipName = "Shop Smart", 
                        Name = "To avoid buying more food than you need, make frequent trips to the grocery store every few days rather than doing a bulk shopping trip once a week.", 
                        TipLikes = 4, TipDislikes = 0, Link = "https://en.wikipedia.org/wiki/Smart_shop", AdminApproved = true},
                new Tip { TipId = 2, TipName = "Store Food Correctly", 
                        Name = "Separating foods that produce more ethylene gas from those that don’t is another great way to reduce food spoilage. Ethylene promotes ripening in foods and could lead to spoilage.", 
                        TipLikes = 4, TipDislikes = 0, Link = "https://www.betterhealth.vic.gov.au/health/healthyliving/food-safety-and-storage", AdminApproved = true},
                new Tip { TipId = 3, TipName = "Learn to Preserve", 
                        Name = "Pickling, drying, canning, fermenting, freezing and curing are all methods you can use to make food last longer, thus reducing waste.", 
                        TipLikes = 1, TipDislikes = 0, Link = "https://www.masterclass.com/articles/a-guide-to-home-food-preservation-how-to-pickle-can-ferment-dry-and-preserve-at-home", AdminApproved = true}
                );
           modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Email = "julius.nar@gmail.com", FirstName = "Julius", LastName = "Narkunas", Password = "JuliusNer1", Role = "user" },
                new User { Id = 2, Email = "danielius.rekus@gmail.com", FirstName = "Danielius", LastName = "Rekus", Password = "Danius123", Role = "admin" },
                new User { Id = 3, Email = "mariuks@gmail.com", FirstName = "Marius", LastName = "Ivanausas", Password = "Jhbj433h", Role = "user"},
                new User { Id = 4, Email = "karolis@gmail.com", FirstName = "Karolis", LastName = "Valkauskas", Password = "Karolis123", Role = "admin"},
                new User { Id = 5, Email = "kajus@outlook.com", FirstName = "Kajus", LastName = "Orsauskas", Password = "Kaj47474p", Role = "user"}
              );
            modelBuilder.Entity<FridgeItem>().HasKey(
                o => new { o.UserId, o.ProductId}
            );
            modelBuilder.Entity<CalendarItem>().HasKey(
                o => new { o.UserId, o.ProductId }
            );
        }
    }
}
