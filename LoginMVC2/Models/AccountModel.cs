using System.ComponentModel.DataAnnotations;

namespace LoginMVC2.Models
{
    public class AccountModel
    {
        public class LogOnModel
        {
            [Required]
            [Display(Name = "Nome de usuário")]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Senha")]
            public string Password { get; set; }

            [Display(Name = "Lembrar?")]
            public bool RememberMe { get; set; }
        }

        public class RegisterModel
        {
            [Required]
            [Display(Name = "Nome de usuário")]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.EmailAddress)]
            [Display(Name = "E-mail")]
            public string Email { get; set; }

            [Required]
            [TamanhoString(100, MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Senha")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm a senha")]
            [Compare("Password", ErrorMessage = "As senhas não conferem")]
            public string ConfirmPassword { get; set; }
        }
    }
}