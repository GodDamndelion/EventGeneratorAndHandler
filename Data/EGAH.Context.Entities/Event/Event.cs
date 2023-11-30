namespace EGAH.Context.Entities;

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

[Index("Id", IsUnique = true)]
public class Event
{
    [Key, Required]
    public Guid Id { get; set; } = Guid.NewGuid();

    public EventTypeEnum Type { get; set; }

    public DateTime Time { get; set; } // Дата генерации события

    public virtual Incident? Incident { get; set; }
}
