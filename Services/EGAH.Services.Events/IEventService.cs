namespace EGAH.Services.Events;

public interface IEventService
{
    Task<EventModel> CreateEvent();
}
