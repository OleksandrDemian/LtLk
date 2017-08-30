using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IPoolable
{
    protected Node currentNode;
    protected int x;
    protected int y;

    public GameObject GetGameObject
    {
        get
        {
            return gameObject;
        }
    }

    public virtual void Initialize()
    {
        x = (int)transform.position.x;
        y = (int)transform.position.z;
        currentNode = MapManager.Instance.GetNode(X, Y);
        currentNode.SetEntity(this);
    }

    public void Initialize(int x, int y)
    {
        transform.position = new Vector3(x, .2f, y);
        EntitiesManager.Instance.AddEntity(this);
        Initialize();
    }

    protected void DisableEntity()
    {
        currentNode.ReleaseEntity();
        EntitiesManager.Instance.RemoveEntity(this);
        ObjectPool.Add(this);
        Debug.Log(name + " is disabled!");
    }

    public abstract void ShowInfo();

    public int X
    {
        get
        {
            return x;
        }
        set
        {
            x = value;
        }
    }

    public int Y
    {
        get
        {
            return y;
        }
        set
        {
            y = value;
        }
    }

    public int Distance(Entity to)
    {
        int dx = to.X - X;
        int dy = to.Y - Y;
        return Mathf.Abs(dx) + Mathf.Abs(dy);
    }

    public abstract void Interact(Entity actor);
    public abstract void Turn();

    protected static bool Success(int probability)
    {
        return Random.Range(0, 100) < probability ? true : false;
    }

    protected static bool Success(int probability, string label)
    {
        bool success = Success(probability);
        if (success)
        {
            Debug.Log(label);
            HistoryTracer.Instance.AddToHistory(label);
        }

        return success;
    }

    public bool HasAsNearby(System.Type type)
    {
        List<Node> nodes = MapManager.Instance.GetAllAdjacentNodes(X, Y);
        for (int i = 0; i < nodes.Count; i++)
        {
            if (!nodes[i].HasEntity())
                continue;

            if (nodes[i].GetEntity().GetType() == type)
                return true;
        }
        return false;
    }

    public Entity GetNearby<T>()
    {
        List<Node> nodes = MapManager.Instance.GetAllAdjacentNodes(X, Y);
        for (int i = 0; i < nodes.Count; i++)
        {
            if (!nodes[i].HasEntity())
                continue;

            if (nodes[i].GetEntity().GetType() == typeof(T))
                return nodes[i].GetEntity();
        }
        return null;
    }

    public static bool IsPlayer(Entity entity)
    {
        if (Player.Instance.GetCharacter() == entity)
            return true;
        return false;
    }
}
