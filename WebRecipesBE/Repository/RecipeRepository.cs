using WebRecipesBE.Data;
using WebRecipesBE.Interfaces;
using WebRecipesBE.Models;

namespace WebRecipesBE.Repository
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly DataContext _context;
        public RecipeRepository(DataContext context)
        {
            _context = context;   
        }

        public bool CreateRecipe(int categoryId,int userId, Recipe recipe)
        {
            
            var category = _context.Categories.Where(c => c.id_Kategorija == categoryId).FirstOrDefault();

            var recipeCategory = new RecipeCategory()
            {
                Category = category,
                Recipe = recipe
            };

            _context.Add(recipeCategory);

            _context.Add(recipe);

            return Save();
        }

        public bool DeleteRecipe(Recipe recipe)
        {
            _context.Remove(recipe);
            return Save();
        }

        public ICollection<Recipe> GetAllRecipes()
        {
            return _context.Recipes.OrderBy(r => r.id_Receptas).ToList();
        }

        public Recipe GetRecipe(int id)
        {
            return _context.Recipes.Where(r => r.id_Receptas == id).FirstOrDefault();
        }

        public int GetUserIdFromRecipe(Recipe recipe)
        {
            return _context.Recipes.Where(r => r.id_Receptas == recipe.id_Receptas).Select(u => u.User.id_Vartotojas).FirstOrDefault();
        }

        public bool RecipeExists(int id)
        {
            return _context.Recipes.Any(r => r.id_Receptas == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateRecipe(Recipe recipe)
        {
            _context.Update(recipe);
            return Save();
        }
    }
}
