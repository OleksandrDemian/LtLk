using UnityEngine;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour
{
    private Text text;

    public void SetText(string text)
    {
        if (!gameObject.activeInHierarchy)
            Enable(true);

        if(this.text == null)
            this.text = GetComponentInChildren<Text>();

        this.text.text = text;
    }

    public void SetChoice(ChoiceWindow window, Choice choice)
    {
        SetText(choice.GetTitle());

        Button btn = GetComponent<Button>();
        btn.onClick.RemoveAllListeners();

        if(choice.HasTrigger())
            btn.onClick.AddListener(choice.Trigger);
        else
            btn.onClick.AddListener(window.Close);
    }

    public void Enable(bool action)
    {
        gameObject.SetActive(action);
    }
}
