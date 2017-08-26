using UnityEngine;
using UnityEngine.UI;

public class HistoryTracer : MonoBehaviour
{
    private Text[] texts;

    public static HistoryTracer Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        Instance = this;
    }

	private void Start ()
    {
        texts = transform.FindChild("HistoryTracer").GetComponentsInChildren<Text>();
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].text = "";
        }
	}

    public void EnableHistoryView()
    {
        Transform h = transform.FindChild("HistoryTracer");
        h.gameObject.SetActive(!h.gameObject.activeInHierarchy);
    }

    public void AddToHistory(string text)
    {
        for (int i = 0; i < texts.Length - 1; i++)
        {
            texts[i].text = texts[i + 1].text;
        }

        texts[texts.Length-1].text = text;
    }
}
