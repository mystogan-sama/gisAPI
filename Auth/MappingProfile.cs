using gisAPI.Persistence.Repositories;
using AutoMapper;

namespace gisAPI.Auth
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterUser.Command, User>();
            CreateMap<User, LoginDto>()
              .ForMember(d => d.accessToken, opt => opt.Ignore());
        }
    }

    public class LoginDto
    {
        public long Id { get; set; }
        public string? Username { get; set; }
        public string? FullName { get; set; }
        public string? role { get; set; }
        public string? accessToken { get; set; }
        public string? avatar { get; set; }
        public string? email { get; set; }
        public dynamic? ability { get; set; }
        public dynamic? extras { get; set; }
    }
}