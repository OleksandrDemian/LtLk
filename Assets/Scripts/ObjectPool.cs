using System.Collections.Generic;
using UnityEngine;

public interface IPoolable {
    GameObject GetGameObject {
        get;
    }
}

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefs;

    private List<IPoolable> poolable;

    public static ObjectPool Instance
    {
        get;
        private set;
    }

    private void Awake() {
        poolable = new List<IPoolable>();
        Instance = this;
    }

    public static void Add(IPoolable obj)
    {
        obj.GetGameObject.SetActive(false);
        Instance.poolable.Add(obj);
    }

    public static T Get<T>()
    {
        for (int i = 0; i < Instance.poolable.Count; i++)
        {
            if(Instance.poolable[i].GetType() == typeof(T))
            {
                IPoolable go = Instance.poolable[i];
                go.GetGameObject.SetActive(true);
                Instance.poolable.Remove(go);
                return (T)go;
            }
        }

        for (int i = 0; i < Instance.prefs.Length; i++)
        {
            T component = Instance.prefs[i].GetComponent<T>();
            if (component == null)
                continue;

            if (component.GetType() == typeof(T))
            {
                GameObject instance = Instantiate(Instance.prefs[i]);
                return instance.GetComponent<T>();
            }   
        }
        throw new System.Exception("There is no " + typeof(T));
    }

    public static GameObject Get(string name)
    {
        for (int i = 0; i < Instance.prefs.Length; i++)
        {
            if (Instance.prefs[i].name == name)
                return Instantiate(Instance.prefs[i]);
        }
        throw new System.Exception("There is no " + name);
    }
}