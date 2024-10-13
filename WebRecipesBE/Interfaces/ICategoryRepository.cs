using WebRecipesBE.Models;

namespace WebRecipesBE.Interfaces
{
    public interface ICategoryRepository
    {
        Category GetCategory(int id);
        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);//reikia paduoti entity o ne tik id !!!! id gausim per parametrus
        ICollection<Category> GetAllCategories();
        bool CategoryExists(int id);
        bool Save();
    }
}
