namespace EGAH.EventGenerator.Controllers.Models;

using AutoMapper;
using EGAH.Context.Entities;
using EGAH.Services.Events;
using FluentValidation;

public class EventRequestResponse
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public EventTypeEnum Type { get; set; }

    public DateTime Time { get; set; } // Дата генерации события
}

public class EventRequestResponseValidator : AbstractValidator<EventRequestResponse>
{
    public EventRequestResponseValidator() { }
}

public class EventRequestResponseProfile : Profile
{
    public EventRequestResponseProfile()
    {
        CreateMap<EventRequestResponse, EventModel>();
        CreateMap<EventModel, EventRequestResponse>();
    }
}
