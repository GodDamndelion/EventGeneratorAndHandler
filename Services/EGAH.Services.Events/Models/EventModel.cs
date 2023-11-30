namespace EGAH.Services.Events;

using AutoMapper;
using EGAH.Context.Entities;
using FluentValidation;

public class EventModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public EventTypeEnum Type { get; set; }

    public DateTime Time { get; set; } // Дата генерации события
}

public class EventModelValidator : AbstractValidator<EventModel>
{
    public EventModelValidator() { }
}

public class EventModelProfile : Profile
{
    public EventModelProfile()
    {
        CreateMap<EventModel, Event>();
        CreateMap<Event, EventModel>();
    }
}
