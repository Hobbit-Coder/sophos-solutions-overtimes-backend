using SophosSolutions.Overtimes.Models.Common;
using SophosSolutions.Overtimes.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SophosSolutions.Overtimes.Models.Entities;

public class Overtime : AuditableEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
    public int Hours { get; set; }
    public string? Description { get; set; }
    public Status Status { get; set; }

    // Objeto necesario para hacer mas estricta una relacion
    [ForeignKey(nameof(UserId))] public User? User { get; set; }

    // Datos obligatorios para crear la entidad
    public Overtime(Guid userId, DateTime date, int hours, Status status)
    {
        UserId = userId;
        Date = date;
        Hours = hours;
        Status = status;
    }
}
