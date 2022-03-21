using System.ComponentModel.DataAnnotations;

namespace ClaimAuthorizationApi.Model.ViewModels.User
{
    public class UserEditViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Please, provied full name.")]
        [StringLength(50, MinimumLength = 2)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please, provied email address.")]
        [StringLength(100, MinimumLength = 11)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please, provied user name.")]
        [StringLength(100, MinimumLength = 2)]
        public string UserName { get; set; }
    }
}
