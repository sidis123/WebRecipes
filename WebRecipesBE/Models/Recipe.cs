using System.ComponentModel.DataAnnotations;

namespace WebRecipesBE.Models
{
    public class Recipe
    {
        [Key]
        public int id_Receptas { get; set; }

        public string pavadinimas { get; set; }
        public string tekstas { get; set; }
        public string instrukcija { get; set; }
    }
}
