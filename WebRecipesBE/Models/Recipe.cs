using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebRecipesBE.Models
{
    public class Recipe
    {
        [Key]
        public int id_Receptas { get; set; }
        [Column(TypeName ="nvarchar(100)")]
        public string pavadinimas { get; set; }
        [Column(TypeName = "nvarchar(254)")]
        public string tekstas { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string instrukcija { get; set; }

        public string PictureUrl { get; set; }

        public int Userid_Vartotojas { get; set; }

        public User User { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<RecipeCategory> ReceptuKategorijos { get; set; }
    }
}
