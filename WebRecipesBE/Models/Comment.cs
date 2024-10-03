using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebRecipesBE.Models
{
    public class Comment
    {
        [Key]
        public int id_Komentaras { get; set; }
        public int patiktukai { get; set; }
        public DateTime sukurimo_data { get; set; }
        [Column(TypeName = "nvarchar(254)")]
        public string tekstas { get; set; }
    }
}
