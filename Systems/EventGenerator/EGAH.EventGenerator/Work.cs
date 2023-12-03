namespace EGAH.EventGenerator.Work;

using EGAH.Services.Events;
using System.Security.Cryptography;

public static class Work
{
    public static async void Do(IServiceProvider serviceProvider)
    {
        var eventService = serviceProvider.GetService<IEventService>()!;
        ArgumentNullException.ThrowIfNull(eventService);
        while (true)
        {
            int milliseconds = RandomNumberGenerator.GetInt32(2001); // От 0 до 2 сек.
            Thread.Sleep(milliseconds);
            
            await eventService.CreateEvent();
        }
    }
}
