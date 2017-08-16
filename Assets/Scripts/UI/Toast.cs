using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class Toast : MonoBehaviour, IPoolable
{
    [SerializeField]
    private Text text;

    public GameObject GetGameObject
    {
        get
        {
            return gameObject;
        }
    }

    public static void ShowToast(string text, int seconds)
    {
        Toast toast = ObjectPool.Get<Toast>();
        toast.SetText(text, seconds);
    }

    public static void ShowToast(string text)
    {
        ShowToast(text, 2);
    }

    public void SetText(string text, int seconds)
    {
        this.text.text = text;
        StartCoroutine(DisableWait(2));
    }

    private IEnumerator DisableWait(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        ObjectPool.Add(this);
    }
}
