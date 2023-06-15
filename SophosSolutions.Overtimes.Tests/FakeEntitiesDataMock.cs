using SophosSolutions.Overtimes.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SophosSolutions.Overtimes.Tests;

public static class FakeEntitiesDataMock
{
    public static Area Area { get; } = new Area("TI")
    {
        Id = Guid.NewGuid(),
        Description = "IT Description",
        CreatedBy = Guid.NewGuid(),
        CreatedOn = DateTime.UtcNow
    };
}
