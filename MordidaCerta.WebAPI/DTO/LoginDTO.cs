using System.ComponentModel.DataAnnotations;


namespace MordidaCerta.WebAPI.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "O Email do usuário é obrigatório!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A Senha do usuário é obrigatória!")]
        public string? Senha { get; set; }
    }
}
