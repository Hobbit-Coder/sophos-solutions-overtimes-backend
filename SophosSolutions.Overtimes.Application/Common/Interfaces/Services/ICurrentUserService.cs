using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SophosSolutions.Overtimes.Application.Common.Interfaces.Services;

public interface ICurrentUserService
{
    Guid UserId { get; }
}
