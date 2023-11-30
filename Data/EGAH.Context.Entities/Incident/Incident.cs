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

    public Guid FirstEventId { get; set; }
    public virtual Event FirstEvent { get; set; } // Event.Type = 1

    public Guid? SecondEventId { get; set; }
    public virtual Event? SecondEvent { get; set; } // Event.Type = 2, если Incident.Type = 2
}
