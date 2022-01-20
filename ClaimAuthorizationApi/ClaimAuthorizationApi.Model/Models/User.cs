using Microsoft.AspNetCore.Identity;

namespace ClaimAuthorizationApi.Model.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime LastModifiedTime { get; set; }
    }
}
