using UnityEngine;

public abstract class Quest : MonoBehaviour
{
    [SerializeField]
    protected string title;
    [SerializeField][TextArea]
    protected string description;

    protected bool done = false;

    public static Quest Instance
    {
        get;
        private set;
    }

    public void ShowQuest()
    {
        InformationWindow.ShowInformation(title, description, false);
    }

    protected virtual void Awake()
    {
        Instance = this;
    }

    public virtual bool Check()
    {
        return done;
    }
}
