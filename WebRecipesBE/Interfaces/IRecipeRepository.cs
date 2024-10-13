using WebRecipesBE.Models;

namespace WebRecipesBE.Interfaces
{
    public interface IRecipeRepository
    {
        bool CreateRecipe(int categoryId,int userId,Recipe recipe);
        bool DeleteRecipe(Recipe recipe);
        bool UpdateRecipe(Recipe recipe);
        Recipe GetRecipe(int id);
        ICollection<Recipe> GetAllRecipes();
        int GetUserIdFromRecipe(Recipe recipe);
        bool RecipeExists(int id);
        bool Save();
    }
}
