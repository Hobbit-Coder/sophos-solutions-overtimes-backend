using AutoMapper;
using SophosSolutions.Overtimes.Application.Roles.Common;
using SophosSolutions.Overtimes.Models.Entities;

namespace SophosSolutions.Overtimes.Application.Common.Mappings;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<Role, GetRoleResponse>();
    }
}
