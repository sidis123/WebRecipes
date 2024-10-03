namespace WebRecipesBE.Models
{
    public class RecipeCategory
    {
        public int RecipeID { get; set; }
        public int CategoryId { get; set; }
        public Recipe Recipe { get; set; }
        public Category Category { get; set; }
    }
}
