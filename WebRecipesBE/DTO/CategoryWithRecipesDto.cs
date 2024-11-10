namespace WebRecipesBE.DTO
{
    public class CategoryWithRecipesDto
    {
        public int id_Kategorija { get; set; }
        public string pavadinimas { get; set; }
        public ICollection<RecipeDto> Receptai { get; set; }
    }
}
