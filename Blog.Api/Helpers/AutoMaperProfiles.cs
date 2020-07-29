using AutoMapper;
using Blog.Api.Dtos;
using Blog.Api.Models;

namespace Blog.Api.Helpers
{
    public class AutoMaperProfiles:Profile
    {
        public AutoMaperProfiles()
        {
            CreateMap<LoginDto,ApplicationUser>().ReverseMap();
            CreateMap<RegistrationDto,ApplicationUser>().ReverseMap();
            CreateMap<ApplicationUser,UserDetailsDto>().ReverseMap();
            CreateMap<BlogToReturnDto,BlogEntity>().ReverseMap();
            CreateMap<BlogCreationDto,BlogEntity>().ReverseMap();
        }
    }
}