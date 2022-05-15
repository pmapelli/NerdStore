using MediatR;

namespace NS.Core.Messages;

public class Event : Message, INotification
{
    public DateTime Timestamp { get; }

    protected Event()
    {
        Timestamp = DateTime.Now;
    }
}