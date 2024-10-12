using System.ComponentModel.DataAnnotations.Schema;

namespace WebRecipesBE.DTO
{
    public class CategoryDto
    {

        public int id_Kategorija { get; set; }
        public string pavadinimas { get; set; }
    }
}
