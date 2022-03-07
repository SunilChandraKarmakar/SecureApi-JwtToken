namespace ClaimAuthorizationApi.Model.ViewModels.User
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Token { get; set; }
    }
}
