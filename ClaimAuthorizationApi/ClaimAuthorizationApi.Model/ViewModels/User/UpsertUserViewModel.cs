using System.ComponentModel.DataAnnotations;

namespace ClaimAuthorizationApi.Model.ViewModels.User
{
    public class UpsertUserViewModel
    {
        [Required(ErrorMessage = "Please, provied full name.")]
        [StringLength(50, MinimumLength = 2)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please, provied email address.")]
        [StringLength(100, MinimumLength = 11)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please, provied password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
