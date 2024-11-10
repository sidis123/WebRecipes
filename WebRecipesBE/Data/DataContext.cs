using Microsoft.EntityFrameworkCore;
using WebRecipesBE.Models;

namespace WebRecipesBE.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<RecipeCategory> RecipeCategories { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring composite primary key for RecipeCategory
            modelBuilder.Entity<RecipeCategory>()
                .HasKey(pc => new { pc.RecipeId, pc.CategoryId });

            // Configuring many-to-many relationship between Recipe and Category
            modelBuilder.Entity<RecipeCategory>()
                .HasOne(p => p.Recipe)
                .WithMany(pc => pc.ReceptuKategorijos)
                .HasForeignKey(p => p.RecipeId);

            modelBuilder.Entity<RecipeCategory>()
                .HasOne(p => p.Category)
                .WithMany(pc => pc.ReceptuKategorijos)
                .HasForeignKey(c => c.CategoryId);

            // Configuring relationships for Comment entity
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)  // Use the navigation property for User
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.Userid_Vartotojas)  // Use the foreign key property for User
                .OnDelete(DeleteBehavior.Restrict);  // Set to Restrict to avoid multiple cascade paths

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Recipe)  // Use the navigation property for Recipe
                .WithMany(r => r.Comments)
                .HasForeignKey(c => c.Recipeid_Receptas)  // Use the foreign key property for Recipe
                .OnDelete(DeleteBehavior.Cascade);  // Cascade on Recipe deletion

            base.OnModelCreating(modelBuilder);

        }
    }
}
