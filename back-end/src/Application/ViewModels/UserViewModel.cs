using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Application.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        [JsonProperty(PropertyName = "name")]
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "email")]
        [Required(ErrorMessage = "O campo e-mail é obrigatório")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Por favor insira o endereço de e-mail correto")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "password")]
        [Required(ErrorMessage = "A senha é obrigatório.")]
        public string Password { get; set; }
        
        [JsonProperty(PropertyName = "repeatPassword")]
        [Required(ErrorMessage = "O campo confirme a nova senha é obrigatório")]
        [Compare("Password", ErrorMessage = "Senha não confere")]
        public string RepeatPassword { get; set; }
    }
}
