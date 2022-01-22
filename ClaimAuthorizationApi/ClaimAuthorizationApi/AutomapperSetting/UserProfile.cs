using AutoMapper;
using ClaimAuthorizationApi.Model.Models;
using ClaimAuthorizationApi.Model.ViewModels.User;

namespace ClaimAuthorizationApi.AutomapperSetting
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UpsertUserViewModel, User>();
            CreateMap<User, UpsertUserViewModel>();
        }
    }
}
