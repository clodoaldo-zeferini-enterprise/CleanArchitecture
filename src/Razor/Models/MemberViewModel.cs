using System.ComponentModel.DataAnnotations;

namespace Razor.Models
{
    public class MemberViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "O Primeiro Nome do Membro é Obrigatório!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "O Último Nome do Membro é Obrigatório!")]
        public string? LastName { get; set; }
        
        [Required(ErrorMessage = "O Gênero do Membro é Obrigatório!")]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "O E-mail do Membro é Obrigatório!")] 
        public string? Email { get; set; }
    }
}
