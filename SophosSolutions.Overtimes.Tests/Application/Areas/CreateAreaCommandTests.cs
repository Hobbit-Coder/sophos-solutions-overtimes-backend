using AutoMapper;
using Moq;
using SophosSolutions.Overtimes.Application.Areas.Commands;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Repositories;
using SophosSolutions.Overtimes.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SophosSolutions.Overtimes.Tests.Application.Areas;

public class CreateAreaCommandTests
{
    [Fact]
    public async Task ShouldCreateAreaCommandTest()
    {
        // Mock
        Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
        Mock<IMapper> mapperMock = new Mock<IMapper>();

        // Arrange
        var command = FakeCommandsDataMock.CreateAreaCommand;
        var handler = new CreateAreaCommandHandler(unitOfWorkMock.Object, mapperMock.Object);
        var area = FakeEntitiesDataMock.Area;

        // Setups
        mapperMock.Setup(mock => mock.Map<Area>(command)).Returns(area);
        unitOfWorkMock.Setup(mock => mock.AreaRepository.Add(area)).Returns(true);
        unitOfWorkMock.Setup(mock => mock.CompleteAsync(CancellationToken.None)).ReturnsAsync(1);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotEqual(Guid.Empty, result);
        Assert.Equal(area.Id, result);

        // Validate
        unitOfWorkMock.Verify(mock => mock.AreaRepository.Add(area));
        unitOfWorkMock.Verify(mock => mock.CompleteAsync(CancellationToken.None));
        mapperMock.Verify(mock => mock.Map<Area>(command), Times.Once);
    }
}
