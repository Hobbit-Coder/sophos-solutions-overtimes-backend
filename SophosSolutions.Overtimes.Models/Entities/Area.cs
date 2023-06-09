using SophosSolutions.Overtimes.Models.Common;

namespace SophosSolutions.Overtimes.Models.Entities;

public class Area : AuditableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }

    // Datos obligatorios para crear la entidad
    public Area(string name)
    {
        Name = name;
    }
}
