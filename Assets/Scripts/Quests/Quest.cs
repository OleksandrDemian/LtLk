using UnityEngine;

public abstract class Quest : MonoBehaviour
{
    [SerializeField]
    protected string title;
    [SerializeField][TextArea]
    protected string description;
    [SerializeField]
    [TextArea]
    protected string doneText;
    [SerializeField]
    protected Item[] items;

    public static Quest Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        Instance = this;
        Initialize();
    }

    public void ShowQuest()
    {
        InformationWindow.ShowInformation(title, description, false);
    }

    public abstract bool Check();
    public abstract void Initialize();

    public string GetDoneText()
    {
        return doneText;
    }

    protected void OnQuestDone()
    {
        string loot = "\n";
        Inventory inv = Player.Instance.GetCharacter().GetInventory();
        int size = items.Length;
        for (int i = 0; i < size; i++)
        {
            Item item = items[i];
            inv.AddItem(item);
            loot += item.GetName() + " " + item.GetQty() + ";\n";
        }

        InformationWindow.ShowInformation(title, doneText + loot, false);
    }

    public string GetTitle()
    {
        return title;
    }

    public string GetDescription()
    {
        return description;
    }
}
