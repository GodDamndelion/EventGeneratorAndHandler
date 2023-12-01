namespace EGAH.EventHandler.Controllers;

using AutoMapper;
using EGAH.Common.Responses;
using EGAH.EventHandler.Controllers.Models;
using EGAH.Services.Incidents;
using Microsoft.AspNetCore.Mvc;
using EGAH.Services.Events;

/// <summary>
/// Incidents controller
/// </summary>
/// <response code="400">Bad Request</response>
/// <response code="401">Unauthorized</response>
/// <response code="403">Forbidden</response>
/// <response code="404">Not Found</response>
[ProducesResponseType(typeof(ErrorResponse), 400)]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/incidents")]
[ApiController]
[ApiVersion("1.0")]
public class IncidentsController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IIncidentService incidentService;

    public IncidentsController(IMapper mapper, IIncidentService incidentService)
    {
        this.mapper = mapper;
        this.incidentService = incidentService;
    }

    /// <summary>
    /// Create incident
    /// </summary>
    /// <response code="204">Event ignored, null returned</response>
    [HttpPost("")]
    public async Task<IncidentRequestResponse?> CreateIncident([FromBody] EventModel model)
    {
        var incident = await incidentService.CreateIncident(model);
        var response = mapper.Map<IncidentRequestResponse?>(incident);

        return response;
    }

    /// <summary>
    /// Get incidents
    /// </summary>
    /// <param name="offset">Offset to the first element</param>
    /// <param name="limit">Count of elements on the page</param>
    /// <response code="200">List of IncidentRequestResponses</response>
    [ProducesResponseType(typeof(IEnumerable<IncidentRequestResponse>), 200)]
    [HttpGet("")]
    public async Task<IEnumerable<IncidentRequestResponse>> GetIncidents([FromQuery] int offset = 0, [FromQuery] int limit = 1000)
    {
        var incidents = await incidentService.GetIncidents(offset, limit);
        var response = mapper.Map<IEnumerable<IncidentRequestResponse>>(incidents);

        return response;
    }
}
