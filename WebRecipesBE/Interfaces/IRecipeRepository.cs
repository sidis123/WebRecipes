using WebRecipesBE.Models;

namespace WebRecipesBE.Interfaces
{
    public interface IRecipeRepository
    {
        bool CreateRecipe(Recipe recipe);
        bool DeleteRecipe(Recipe recipe);
        bool UpdateRecipe(Recipe recipe);
        Recipe GetRecipe(int id);
        ICollection<Recipe> GetAllRecipes();
        bool RecipeExists(int id);
        bool Save();
    }
}
