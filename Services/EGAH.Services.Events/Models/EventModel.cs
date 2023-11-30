namespace EGAH.Services.Events;

using AutoMapper;
using EGAH.Context.Entities;

public class EventModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public EventTypeEnum Type { get; set; }

    public DateTime Time { get; set; } // Дата генерации события
}

public class EventModelProfile : Profile
{
    public EventModelProfile()
    {
        CreateMap<EventModel, Event>();
        CreateMap<Event, EventModel>();
    }
}
