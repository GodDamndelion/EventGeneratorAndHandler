namespace EGAH.Services.Incidents;

using EGAH.Services.Events;

public interface IIncidentService
{
    Task<IncidentModel?> CreateIncident(EventModel eventModel);
    Task<IEnumerable<IncidentModel>> GetIncidents(int offset = 0, int limit = 1000);
}
