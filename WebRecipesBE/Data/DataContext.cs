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


    }
}
