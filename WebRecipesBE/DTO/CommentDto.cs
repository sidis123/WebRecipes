using System.ComponentModel.DataAnnotations.Schema;

namespace WebRecipesBE.DTO
{
    public class CommentDto
    {

        public int id_Komentaras { get; set; }
        public int patiktukai { get; set; }
        public DateTime sukurimo_data { get; set; }
        public string tekstas { get; set; }


    }
}
