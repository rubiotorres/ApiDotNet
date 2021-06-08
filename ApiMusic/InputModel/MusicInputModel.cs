using System.ComponentModel.DataAnnotations;

namespace ApiMusic.InputModel
{
    public class MusicInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome da musica deve conter entre 3 e 100 caracteres")]
        public string Nome { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome da artista deve conter entre 3 e 100 caracteres")]
        public string Artista { get; set; }
        [Required]
        public bool Premium { get; set; }
    }
}
