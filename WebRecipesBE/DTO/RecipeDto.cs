using System.ComponentModel.DataAnnotations.Schema;

namespace WebRecipesBE.DTO
{
    public class RecipeDto
    {
        public string pavadinimas { get; set; }
        public string tekstas { get; set; }
        public string instrukcija { get; set; }
        public string PictureUrl { get; set; }
    }
}
