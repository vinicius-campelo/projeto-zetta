using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeetGroupAPI.Models
{
    [Serializable]
    public class TokenModel
    {
        [Required(ErrorMessage = "Email é obrigatório!")]
        [EmailAddress(ErrorMessage = "Email com formato invalido!")]
        [StringLength(150, ErrorMessage = "Email deve ter no maximo {1} caracteres.", MinimumLength = 1)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório!")]
        [StringLength(50, ErrorMessage = "Password deve ter no maximo {1} caracteres.", MinimumLength = 1)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Senha { get; set; }
    }


    public class UsuarioCadastroModel
    {
        [Required(ErrorMessage = "Nome é obrigatório!")]
        [StringLength(150, ErrorMessage = "O nome deve ter no maximo {1} caracteres.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Email é obrigatório!")]
        [EmailAddress(ErrorMessage = "Email com formato invalido!")]
        [StringLength(150, ErrorMessage = "Email deve ter no maximo {1} caracteres.", MinimumLength = 1)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "O Telefone é obrigatório!")]
        [StringLength(14, ErrorMessage = "O Telefone deve ter no maximo {1} caracteres.", MinimumLength = 1)]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }


        [Required(ErrorMessage = "Senha é obrigatório!")]
        [StringLength(50, ErrorMessage = "Password deve ter no maximo {1} caracteres.", MinimumLength = 1)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Senha { get; set; }
    }
}
