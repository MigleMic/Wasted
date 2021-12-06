﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Wasted.API.Data;

namespace WastedAPI.Migrations
{
    [DbContext(typeof(WastedContext))]
    partial class WastedContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Wasted.API.Models.Dish", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("numberOfIngredients")
                        .HasMaxLength(3)
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Dishes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "TEST1",
                            Type = "Baked",
                            numberOfIngredients = 2
                        },
                        new
                        {
                            Id = 2,
                            Name = "TEST2",
                            Type = "Baked",
                            numberOfIngredients = 2
                        });
                });

            modelBuilder.Entity("Wasted.API.Models.Ingredient", b =>
                {
                    b.Property<int>("DishId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.HasKey("DishId", "ProductId");

                    b.ToTable("Ingredients");

                    b.HasData(
                        new
                        {
                            DishId = 1,
                            ProductId = 1,
                            Amount = 1
                        },
                        new
                        {
                            DishId = 1,
                            ProductId = 3,
                            Amount = 2
                        },
                        new
                        {
                            DishId = 2,
                            ProductId = 4,
                            Amount = 3
                        },
                        new
                        {
                            DishId = 2,
                            ProductId = 5,
                            Amount = 3
                        });
                });

            modelBuilder.Entity("Wasted.API.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("EnergyValue")
                        .HasColumnType("float");

                    b.Property<string>("MeasurementUnits")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EnergyValue = 158.48699999999999,
                            MeasurementUnits = "kg",
                            Name = "Apple",
                            Type = "Fruit"
                        },
                        new
                        {
                            Id = 2,
                            EnergyValue = 284.54599999999999,
                            MeasurementUnits = "kg",
                            Name = "Troat",
                            Type = "Fish"
                        },
                        new
                        {
                            Id = 3,
                            EnergyValue = 120.69199999999999,
                            MeasurementUnits = "g",
                            Name = "Orange",
                            Type = "Fruit"
                        },
                        new
                        {
                            Id = 4,
                            EnergyValue = 262.178,
                            MeasurementUnits = "kg",
                            Name = "Blackberry",
                            Type = "Berry"
                        },
                        new
                        {
                            Id = 5,
                            EnergyValue = 352.69799999999998,
                            MeasurementUnits = "kg",
                            Name = "Cheese",
                            Type = "Dairy"
                        },
                        new
                        {
                            Id = 6,
                            EnergyValue = 271.745,
                            MeasurementUnits = "kg",
                            Name = "Bass",
                            Type = "Fish"
                        },
                        new
                        {
                            Id = 7,
                            EnergyValue = 175.12450000000001,
                            MeasurementUnits = "l",
                            Name = "Buttermilk",
                            Type = "Dairy"
                        });
                });

            modelBuilder.Entity("Wasted.API.Models.Tip", b =>
                {
                    b.Property<int>("TipId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("TipDislikes")
                        .HasColumnType("int");

                    b.Property<int>("TipLikes")
                        .HasColumnType("int");

                    b.Property<string>("TipName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("TipId");

                    b.ToTable("Tips");

                    b.HasData(
                        new
                        {
                            TipId = 1,
                            Link = "https://en.wikipedia.org/wiki/Smart_shop",
                            Name = "To avoid buying more food than you need, make frequent trips to the grocery store every few days rather than doing a bulk shopping trip once a week.",
                            TipDislikes = 0,
                            TipLikes = 4,
                            TipName = "Shop Smart"
                        },
                        new
                        {
                            TipId = 2,
                            Link = "https://www.betterhealth.vic.gov.au/health/healthyliving/food-safety-and-storage",
                            Name = "Separating foods that produce more ethylene gas from those that don’t is another great way to reduce food spoilage. Ethylene promotes ripening in foods and could lead to spoilage.",
                            TipDislikes = 0,
                            TipLikes = 4,
                            TipName = "Store Food Correctly"
                        },
                        new
                        {
                            TipId = 3,
                            Link = "https://www.masterclass.com/articles/a-guide-to-home-food-preservation-how-to-pickle-can-ferment-dry-and-preserve-at-home",
                            Name = "Pickling, drying, canning, fermenting, freezing and curing are all methods you can use to make food last longer, thus reducing waste.",
                            TipDislikes = 0,
                            TipLikes = 1,
                            TipName = "Learn to Preserve"
                        });
                });

            modelBuilder.Entity("Wasted.API.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Email = "mail1",
                            FirstName = "First1",
                            LastName = "Last",
                            Password = "pass1",
                            Role = "user"
                        },
                        new
                        {
                            UserId = 2,
                            Email = "mail2",
                            FirstName = "First2",
                            LastName = "Last1",
                            Password = "pass2",
                            Role = "user"
                        },
                        new
                        {
                            UserId = 3,
                            Email = "mail3",
                            FirstName = "First",
                            LastName = "Last2",
                            Password = "pass3",
                            Role = "admin"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
