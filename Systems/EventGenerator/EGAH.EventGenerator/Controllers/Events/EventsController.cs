namespace EGAH.EventGenerator.Controllers;

using AutoMapper;
using EGAH.Common.Responses;
using EGAH.EventGenerator.Controllers.Models;
using EGAH.Services.Events;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Events controller
/// </summary>
/// <response code="400">Bad Request</response>
/// <response code="401">Unauthorized</response>
/// <response code="403">Forbidden</response>
/// <response code="404">Not Found</response>
[ProducesResponseType(typeof(ErrorResponse), 400)]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/events")]
[ApiController]
[ApiVersion("1.0")]
public class EventsController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IEventService eventService;

    public EventsController(IMapper mapper, IEventService eventService)
    {
        this.mapper = mapper;
        this.eventService = eventService;
    }

    /// <summary>
    /// Create event and send it to incident
    /// </summary>
    [HttpPost("")]
    public async Task<EventRequestResponse> CreateEvent()
    {
        var newEvent = await eventService.CreateEvent();
        var response = mapper.Map<EventRequestResponse>(newEvent);

        return response;
    }
}
