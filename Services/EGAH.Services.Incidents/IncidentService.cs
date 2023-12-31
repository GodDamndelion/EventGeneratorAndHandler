﻿namespace EGAH.Services.Incidents;

using AutoMapper;
using EGAH.Common.Validator;
using EGAH.Context;
using EGAH.Context.Entities;
using EGAH.Services.Events;
using Microsoft.EntityFrameworkCore;

public class IncidentService : IIncidentService
{
    private readonly IDbContextFactory<MainDbContext> contextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<IncidentModel> incidentModelValidator;

    public IncidentService(
        IDbContextFactory<MainDbContext> contextFactory,
        IMapper mapper,
        IModelValidator<IncidentModel> incidentModelValidator
        )
    {
        this.contextFactory = contextFactory;
        this.mapper = mapper;
        this.incidentModelValidator = incidentModelValidator;
    }

    static EventModel? lastSecondEvent = null;

    public async Task<IncidentModel?> CreateIncident(EventModel eventModel)
    {
        IncidentModel? model = null;

        if (eventModel.Type == EventTypeEnum.Second)
        {
            lastSecondEvent = eventModel;
        }
        else if (eventModel.Type == EventTypeEnum.First)
        {
            model = new IncidentModel()
            {
                Type = IncidentTypeEnum.First,
                Time = DateTime.UtcNow,
                FirstEventId = eventModel.Id
            };

            if (lastSecondEvent != null && (eventModel.Time - lastSecondEvent.Time).TotalSeconds <= 20)
            {
                model.Type = IncidentTypeEnum.Second;
                model.SecondEventId = lastSecondEvent.Id;
            }

            incidentModelValidator.Check(model);

            using var context = await contextFactory.CreateDbContextAsync();

            var incident = mapper.Map<Incident>(model);

            await context.Incidents.AddAsync(incident);
            context.SaveChanges();

            lastSecondEvent = null; // Либо это событие уже было использовано, либо уже прошёл срок, либо и было null.
        }

        return model;
    }

    public async Task<IEnumerable<IncidentModel>> GetIncidents(int offset = 0, int limit = 1000)
    {
        using var context = await contextFactory.CreateDbContextAsync();

        var incidents = context
            .Incidents
            .AsQueryable();

        incidents = incidents
            .Skip(Math.Max(offset, 0))
            .Take(Math.Max(0, Math.Min(limit, 10000)));

        var data = (await incidents.ToListAsync()).Select(incident => mapper.Map<IncidentModel>(incident));

        return data;
    }
}
