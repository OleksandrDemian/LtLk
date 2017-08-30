using System.Collections.Generic;
using UnityEngine;

public delegate void OnObjectDisabled(GameObject target);

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

    public event OnObjectDisabled onObjectDisabled;

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
        if(Instance.onObjectDisabled != null)
            Instance.onObjectDisabled(obj.GetGameObject);
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
            if (Instance.prefs[i].name.ToLower() == name.ToLower())
                return Instantiate(Instance.prefs[i]);
        }
        throw new System.Exception("There is no " + name);
    }

    public static GameObject Get(string name, bool newObj)
    {
        if (!newObj)
        {
            for (int i = 0; i < Instance.poolable.Count; i++)
            {
                if (Instance.poolable[i].GetGameObject.name.ToLower() == name.ToLower())
                {
                    GameObject obj = Instance.poolable[i].GetGameObject;
                    obj.SetActive(true);
                    return obj;
                }
            }
        }

        for (int i = 0; i < Instance.prefs.Length; i++)
        {
            if (Instance.prefs[i].name.ToLower() == name.ToLower())
                return Instantiate(Instance.prefs[i]);
        }
        throw new System.Exception("There is no " + name);
    }
}