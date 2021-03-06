﻿public delegate void OnEventTrigger();

public class Event
{
    private OnEventTrigger onEventTrigger;

    public Event()
    {
        EventsManager.Instance.AddEvent(this);
    }

    public void Trigger()
    {
        if (onEventTrigger != null)
            onEventTrigger();
    }

    public void SetOnEventTriggerMethod(OnEventTrigger trigger)
    {
        onEventTrigger = trigger;
    }
}
