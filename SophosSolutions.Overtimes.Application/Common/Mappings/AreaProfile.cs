using AutoMapper;
using SophosSolutions.Overtimes.Application.Areas.Commands;
using SophosSolutions.Overtimes.Models.Entities;

namespace SophosSolutions.Overtimes.Application.Common.Mappings;

public class AreaProfile : Profile
{
    public AreaProfile()
    {
        CreateMap<CreateAreaCommand, Area>();
    }
}
