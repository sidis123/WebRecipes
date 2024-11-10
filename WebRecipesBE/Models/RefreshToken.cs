using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebRecipesBE.Models
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime Expiration { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
