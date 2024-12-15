using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebRecipesBE.Models
{
    public class Category
    {
        [Key]
        public int id_Kategorija { get; set; }
        [Column(TypeName = "nvarchar(5000)")]
        public string pavadinimas { get; set; }

        public ICollection<RecipeCategory> ReceptuKategorijos { get; set; }

    }
}
