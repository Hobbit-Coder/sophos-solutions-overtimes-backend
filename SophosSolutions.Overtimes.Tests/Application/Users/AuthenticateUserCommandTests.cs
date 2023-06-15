using Moq;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Repositories;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Services;
using SophosSolutions.Overtimes.Application.Users.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SophosSolutions.Overtimes.Tests.Application.Users;

public class AuthenticateUserCommandTests
{
    [Fact]
    public async Task ShouldAuthenticateUserCommandTest()
    {
        // Mock
        Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
        Mock<IJwtTokenGeneratorService> jwtServiceMock = new Mock<IJwtTokenGeneratorService>();

        // Arrange
        var command = FakeCommandsDataMock.AuthenticateUserCommand;
        var handler = new AuthenticateUserCommandHandler(unitOfWorkMock.Object, jwtServiceMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);

        // Validations
        unitOfWorkMock.Verify();
        jwtServiceMock.Verify();
    }
}
