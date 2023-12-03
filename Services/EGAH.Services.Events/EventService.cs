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
    private readonly EventServiceSettings settings;
    private readonly IDbContextFactory<MainDbContext> contextFactory;
    private readonly IMapper mapper;
    private readonly IHttpClientFactory _httpClientFactory;

    public EventService(
        EventServiceSettings settings,
        IDbContextFactory<MainDbContext> contextFactory,
        IMapper mapper,
        IHttpClientFactory httpClientFactory
        )
    {
        this.settings = settings;
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

        using HttpClient client = _httpClientFactory.CreateClient();
        string url = $"{settings.EventHandlerRoot}/v1/incidents";
        //string url = "http://host.docker.internal:10001/api/v1/incidents"; Внутри Докера используется host.docker.internal вместо localhost!!!

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
