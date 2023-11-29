namespace EGAH.Context.Entities;

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

[Index("Id", IsUnique = true)]
public class Incident
{
    [Key, Required]
    public Guid Id { get; set; } = Guid.NewGuid();

    public IncidentTypeEnum Type { get; set; }

    public DateTime Time { get; set; } // Дата создания инцидента
}
