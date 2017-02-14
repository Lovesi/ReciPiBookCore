using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ReciPiBook.Entities
{
    public class RecipesDb : IdentityDbContext<ApplicationUser>
    {
        public RecipesDb(DbContextOptions<RecipesDb> options)
            : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<UnitOfMeasure> UnitsOfMeasure { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseOpenIddict();

            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasKey(i => new { i.LoginProvider, i.ProviderKey });

            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasKey(i => new { i.UserId, i.RoleId });

            modelBuilder.Entity<IdentityUserToken<string>>()
                .HasKey(x => new { x.UserId, x.LoginProvider, x.Name });

            modelBuilder.Entity<Ingredient>()
                .HasOne(i => i.Recipe)
                .WithMany(r => r.Ingredients)
                .HasForeignKey(i => i.RecipeId)
                .IsRequired();

            modelBuilder.Entity<Step>()
                .HasOne(s => s.Recipe)
                .WithMany(r => r.Steps)
                .HasForeignKey(s => s.RecipeId)
                .IsRequired();
        }
    }
}