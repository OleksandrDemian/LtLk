using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InformationWindow : MonoBehaviour, IPoolable
{
    [SerializeField]
    private Text title;
    [SerializeField]
    private Text body;
    private bool isEvent = false;
    private string wtag = "";
    private static List<string> activeWindows = new List<string>();

    public static void ShowInformation(string title, string body)
    {
        Event e = new Event();
        e.SetOnEventTriggerMethod(delegate()
        {
            InformationWindow window = ObjectPool.Get<InformationWindow>();
            window.Show(title, body);
            window.isEvent = true;
            window.GetComponent<Canvas>().sortingOrder = 100;
        });
    }

    public static void ShowInformation(string title, string body, bool isEvent)
    {
        ShowInformation(title, body, isEvent, "default");
    }

    public static void ShowInformation(string title, string body, bool isEvent, string tag)
    {
        if (isEvent)
        {
            ShowInformation(title, body);
        }
        else
        {
            if (activeWindows.Contains(tag))
                return;

            InformationWindow window = ObjectPool.Get<InformationWindow>();
            window.Show(title, body);
            window.isEvent = false;
            window.wtag = tag;
            window.GetComponent<Canvas>().sortingOrder = 101;
            activeWindows.Add(tag);
        }
    }

    public void Show(string title, string body)
    {
        this.title.text = title;
        this.body.text = body;
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
        else
            activeWindows.Remove(wtag);
    }
}
