using System;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo e-mail é obrigatório")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Por favor insira o endereço de e-mail correto")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatório.")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "O campo confirme a nova senha é obrigatório")]
        [Compare("Password", ErrorMessage = "Senha não confere")]
        public string RepeatPassword { get; set; }
    }
}
