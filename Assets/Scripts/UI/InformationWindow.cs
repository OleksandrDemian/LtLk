using UnityEngine;
using UnityEngine.UI;

public class InformationWindow : MonoBehaviour, IPoolable
{
    [SerializeField]
    private Text title;
    [SerializeField]
    private Text body;
    private bool isEvent = false;

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
        if (isEvent)
        {
            ShowInformation(title, body);
        }
        else
        {
            InformationWindow window = ObjectPool.Get<InformationWindow>();
            window.Show(title, body);
            window.isEvent = false;
            window.GetComponent<Canvas>().sortingOrder = 101;
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
        if(isEvent)
            EventManager.Instance.Next();
    }
}
