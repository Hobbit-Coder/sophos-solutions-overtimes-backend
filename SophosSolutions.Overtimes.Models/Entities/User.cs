using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SophosSolutions.Overtimes.Models.Entities;

public class User : IdentityUser<Guid>
{
    // Extension de los atributos de la entidad base, se pueden agregar propiedades adicionales para extender la entidad
    public string Name { get; set; }
    public string LastName { get; set; }
    public Guid? AreaId { get; set; }
    public Guid? ManagerId { get; set; }

    // Objetos necesarios para hacer mas estricta una relacion
    [ForeignKey(nameof(AreaId))] public Area? Area { get; set; }
    [ForeignKey(nameof(ManagerId))] public User? Manager { get; set; }

    // Datos obligatorios para crear la entidad
    public User(string name, string lastName)
    {
        Name = name;
        LastName = lastName;
    }
}
