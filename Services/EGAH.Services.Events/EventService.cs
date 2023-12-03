namespace EGAH.Services.Events;

using AutoMapper;
using EGAH.Context;
using EGAH.Context.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

public class EventService : IEventService
{
    private readonly IDbContextFactory<MainDbContext> contextFactory;
    private readonly IMapper mapper;
    private readonly IHttpClientFactory _httpClientFactory;

    public EventService(
        IDbContextFactory<MainDbContext> contextFactory,
        IMapper mapper,
        IHttpClientFactory httpClientFactory
        )
    {
        this.contextFactory = contextFactory;
        this.mapper = mapper;
        this._httpClientFactory = httpClientFactory;
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

        //string eventHandlerRoot = "http://localhost:10001/api";

        using HttpClient client = _httpClientFactory.CreateClient();
        //string url = $"{eventHandlerRoot}/v1/incidents";
        string url = "http://host.docker.internal:10001/api/v1/incidents";

        var body = JsonSerializer.Serialize(model);
        var request = new StringContent(body, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(url, request);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        return model;
    }
}
