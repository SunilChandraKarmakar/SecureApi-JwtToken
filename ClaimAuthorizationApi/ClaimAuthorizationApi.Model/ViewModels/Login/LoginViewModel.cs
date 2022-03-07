using System.ComponentModel.DataAnnotations;

namespace ClaimAuthorizationApi.Model.ViewModels.Login
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please, provied email address.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please, provied password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
