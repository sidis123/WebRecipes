﻿using System.ComponentModel.DataAnnotations.Schema;

namespace WebRecipesBE.DTO
{
    public class CommentDto
    {
        public int Recipeid_Receptas { get; set; }

        public int Userid_Vartotojas { get; set; }
        public int id_Komentaras { get; set; }
        public int patiktukai { get; set; }
        public DateTime sukurimo_data { get; set; }
        public string tekstas { get; set; }
  
    }
}
