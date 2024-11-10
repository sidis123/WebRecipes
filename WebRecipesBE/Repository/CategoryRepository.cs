using Microsoft.EntityFrameworkCore;
using WebRecipesBE.Data;
using WebRecipesBE.Interfaces;
using WebRecipesBE.Models;

namespace WebRecipesBE.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }
        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(r => r.id_Kategorija == id);
        }

        public bool CreateCategory(Category category)
        {
            _context.Add(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _context.Remove(category);
            return Save();
        }

        public ICollection<Category> GetAllCategories()
        {
            return _context.Categories.OrderBy(r => r.id_Kategorija).ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Where(r => r.id_Kategorija == id).FirstOrDefault();
        }

        public Category GetCategoryWithRecipes(int id)
        {
            return _context.Categories.Include(c => c.ReceptuKategorijos).ThenInclude(rc => rc.Recipe).FirstOrDefault(c => c.id_Kategorija == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }

    }
}
