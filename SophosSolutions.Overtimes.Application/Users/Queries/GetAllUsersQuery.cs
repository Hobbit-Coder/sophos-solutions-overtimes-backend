using MediatR;
using SophosSolutions.Overtimes.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SophosSolutions.Overtimes.Application.Users.Queries;

public class GetAllUsersQuery : IRequest<IEnumerable<User>>
{
}
