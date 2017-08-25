using UnityEngine;
using UnityEngine.UI;

public class ChoiceWindow : MonoBehaviour, IPoolable
{
    protected ChoiceButton[] choices;
    protected bool isEvent = true;

    [SerializeField]
    private Text title;
    [SerializeField]
    private Text description;

    public static void Open(string title, string description, Choice[] choices)
    {
        Event e = new Event();
        e.SetOnEventTriggerMethod(delegate ()
        {
            ChoiceWindow window = ObjectPool.Get<ChoiceWindow>();
            window.SetTitle(title);
            window.SetDescription(description);
            window.SetChoices(choices);
        });
    }

    private void GetChoices()
    {
        choices = GetComponentsInChildren<ChoiceButton>();
    }

    public void SetTitle(string title)
    {
        this.title.text = title;
    }

    public void SetDescription(string description)
    {
        this.description.text = description;
    }

    public void SetChoices(Choice[] c)
    {
        if (choices == null)
            GetChoices();

        for (int i = 0; i < choices.Length; i++)
        {
            if (i < c.Length)
            {
                choices[i].SetChoice(this, c[i]);
            }
            else
            {
                choices[i].Enable(false);
            }
        }
    }

    public void SetIsEvent(bool isEvent)
    {
        this.isEvent = isEvent;
    }

    public GameObject GetGameObject
    {
        get { return gameObject; }
    }

    public void Close()
    {
        ObjectPool.Add(this);
        if (isEvent)
            EventsManager.Instance.Next();
    }
}
