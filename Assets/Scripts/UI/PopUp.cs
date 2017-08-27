using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour, IPoolable
{
    public GameObject GetGameObject
    {
        get
        {
            return gameObject;
        }
    }
    private float time = 0f;
    [SerializeField]
    private Text text;

    public static void Show(Vector3 position, string text)
    {
        PopUp popup = ObjectPool.Get<PopUp>();
        popup.Initialize(position, text);
    }
	
	public void Update ()
    {
        transform.Translate(Vector2.up * Time.deltaTime);

        if (time < 1)
            time += Time.deltaTime;
        else
            ObjectPool.Add(this);
	}

    public void Initialize(Vector3 position, string text)
    {
        transform.position = position + Vector3.up;
        this.text.text = text;
        time = 0f;
    }
}
