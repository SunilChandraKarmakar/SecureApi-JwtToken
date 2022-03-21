using AutoMapper;
using ClaimAuthorizationApi.Model.Models;
using ClaimAuthorizationApi.Model.ViewModels.Register;
using ClaimAuthorizationApi.Model.ViewModels.User;

namespace ClaimAuthorizationApi.AutomapperSetting
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<UserViewModel, User>();

            CreateMap<RegisterViewModel, User>();
            CreateMap<User, RegisterViewModel>();

            CreateMap<User, UserEditViewModel>();
            CreateMap<UserEditViewModel, User>();
        }
    }
}
