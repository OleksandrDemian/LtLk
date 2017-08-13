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

        btn.onClick.AddListener(choice.Trigger);
        btn.onClick.AddListener(window.Close);
    }

    public void Enable(bool action)
    {
        gameObject.SetActive(action);
    }
}
