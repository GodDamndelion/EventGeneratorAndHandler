namespace EGAH.Services.Events;

using AutoMapper;
using EGAH.Context;
using EGAH.Context.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

public class EventService : IEventService
{
    private readonly IDbContextFactory<MainDbContext> contextFactory;
    private readonly IMapper mapper;

    public EventService(
        IDbContextFactory<MainDbContext> contextFactory,
        IMapper mapper
        )
    {
        this.contextFactory = contextFactory;
        this.mapper = mapper;
    }

    public async Task<EventModel> CreateEvent()
    {
        var model = new EventModel()
        {
            Type = (EventTypeEnum)(RandomNumberGenerator.GetInt32(Enum.GetValues(typeof(EventTypeEnum)).Length) + 1),
            Time = DateTime.UtcNow
        };

        using var context = await contextFactory.CreateDbContextAsync();
        
        var newEvent = mapper.Map<Event>(model);

        await context.Events.AddAsync(newEvent);
        context.SaveChanges();

        // TODO: Дёргать IncidentService
        

        return model;
    }
}
