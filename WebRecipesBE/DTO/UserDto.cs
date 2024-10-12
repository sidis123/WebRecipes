using System.ComponentModel.DataAnnotations.Schema;

namespace WebRecipesBE.DTO
{
    public class UserDto
    {
        public int id_Vartotojas { get; set; }
        public string vardas { get; set; }
        public string pavarde { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string telefonas { get; set; }
        public int role { get; set; }
    }
}
