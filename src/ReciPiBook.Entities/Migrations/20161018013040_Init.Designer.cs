using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ReciPiBook.Entities;

namespace ReciPiBook.Entities.Migrations
{
    [DbContext(typeof(RecipesDb))]
    [Migration("20161018013040_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ReciPiBook.Entities.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<bool>("Optional");

                    b.Property<decimal>("Quantity");

                    b.Property<int>("Rank");

                    b.Property<int>("RecipeId");

                    b.Property<int?>("UomId");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.HasIndex("UomId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("ReciPiBook.Entities.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int?>("MinutesToTable");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Notes");

                    b.Property<int?>("Servings");

                    b.HasKey("Id");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("ReciPiBook.Entities.Step", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Optional");

                    b.Property<int>("Rank");

                    b.Property<int>("RecipeId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.ToTable("Steps");
                });

            modelBuilder.Entity("ReciPiBook.Entities.UnitOfMeasure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abbreviation")
                        .HasAnnotation("MaxLength", 10);

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("UnitsOfMeasure");
                });

            modelBuilder.Entity("ReciPiBook.Entities.Ingredient", b =>
                {
                    b.HasOne("ReciPiBook.Entities.Recipe", "Recipe")
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ReciPiBook.Entities.UnitOfMeasure", "Uom")
                        .WithMany()
                        .HasForeignKey("UomId");
                });

            modelBuilder.Entity("ReciPiBook.Entities.Step", b =>
                {
                    b.HasOne("ReciPiBook.Entities.Recipe", "Recipe")
                        .WithMany("Steps")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
