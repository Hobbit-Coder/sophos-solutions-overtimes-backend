using AutoMapper;
using SophosSolutions.Overtimes.Application.Users.Commands;
using SophosSolutions.Overtimes.Application.Users.Common;
using SophosSolutions.Overtimes.Models.Entities;

namespace SophosSolutions.Overtimes.Application.Common.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserCommand, User>();
        CreateMap<User, CreateUserReponse>();
        CreateMap<User, GetUserResponse>();
    }
}
