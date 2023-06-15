using SophosSolutions.Overtimes.Application.Areas.Commands;
using SophosSolutions.Overtimes.Application.Users.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SophosSolutions.Overtimes.Tests;

public static class FakeCommandsDataMock
{
    public static AuthenticateUserCommand AuthenticateUserCommand { get; } = new AuthenticateUserCommand("admin@test.com", "Admin1234*");

    public static CreateAreaCommand CreateAreaCommand { get; } = new CreateAreaCommand("IT")
    {
        Description = "IT Description"
    };
}
