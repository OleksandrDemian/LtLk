public class Choice
{
    private string title;
    private OnEventTrigger onTrigger;

    public Choice(string title, OnEventTrigger onTrigger)
    {
        this.title = title;
        this.onTrigger = onTrigger;
    }

    public string GetTitle()
    {
        return title;
    }

    public void Trigger()
    {
        if (onTrigger != null)
        {
            onTrigger();
        }
    }

    public bool HasTrigger()
    {
        return onTrigger == null ? false : true;
    }
}
