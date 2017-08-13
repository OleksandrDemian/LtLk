using System.Collections.Generic;

public class EventManager
{
    private List<Event> events;
    private int index = 0;
    private IEventListener listener;

    public static EventManager Instance
    {
        get;
        private set;
    }

    public EventManager()
    {
        Instance = this;
        events = new List<Event>();
    }

    public void AddEvent(Event e)
    {
        events.Add(e);
    }

    public void RemoveEvent(Event e)
    {
        if(events.Contains(e))
            events.Remove(e);
    }

    public void ResetEvents()
    {
        index = 0;
        events.Clear();
    }

    public void Next()
    {
        if (index == events.Count)
        {
            if (listener != null)
                listener.OnEventsEnd();
            return;
        }

        events[index].Trigger();
        index++;
    }

    public void SetListener(IEventListener listener)
    {
        this.listener = listener;
    }
}
