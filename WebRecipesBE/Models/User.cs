using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebRecipesBE.Models
{
    public class User
    {
        [Key]
        public int id_Vartotojas { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string vardas { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string pavarde { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string email { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string password { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string telefonas { get; set; }
        public int role { get; set; }
    }

}
