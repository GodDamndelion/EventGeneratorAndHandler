namespace EGAH.Services.Events;

using AutoMapper;
using EGAH.Common.Validator;
using EGAH.Context;
using EGAH.Context.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

public class EventService : IEventService
{
    private readonly IDbContextFactory<MainDbContext> contextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<EventModel> eventModelValidator;

    public EventService(
        IDbContextFactory<MainDbContext> contextFactory,
        IMapper mapper,
        IModelValidator<EventModel> eventModelValidator
        )
    {
        this.contextFactory = contextFactory;
        this.mapper = mapper;
        this.eventModelValidator = eventModelValidator;
    }

    public async Task<EventModel> CreateEvent()
    {
        var model = new EventModel()
        {
            Type = (EventTypeEnum)(RandomNumberGenerator.GetInt32(Enum.GetValues(typeof(EventTypeEnum)).Length) + 1),
            Time = DateTime.UtcNow
        };

        eventModelValidator.Check(model);

        using var context = await contextFactory.CreateDbContextAsync();
        
        var newEvent = mapper.Map<Event>(model);

        await context.Events.AddAsync(newEvent);
        context.SaveChanges();

        // TODO: Дёргать IncidentService

        return model;
    }
}
