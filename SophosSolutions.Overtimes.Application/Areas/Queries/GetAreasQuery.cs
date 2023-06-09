using MediatR;
using SophosSolutions.Overtimes.Models.Entities;

namespace SophosSolutions.Overtimes.Application.Areas.Queries;

public class GetAreasQuery : IRequest<IEnumerable<Area>>
{
}
