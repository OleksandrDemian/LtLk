using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected Node currentNode;

    protected virtual void Start()
    {
        currentNode = MapManager.Instance.GetNode(X, Y);
        currentNode.SetEntity(this);
        EntitiesManager.Instance.AddEntity(this);
    }

    protected virtual void OnDisable()
    {
        DisableEntity();
    }

    protected void DisableEntity()
    {
        currentNode.ReleaseEntity();
        EntitiesManager.Instance.RemoveEntity(this);
    }

    public abstract void ShowInfo();

    public int X
    {
        get
        {
            return (int)transform.position.x;
        }
    }

    public int Y
    {
        get
        {
            return (int)transform.position.z;
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
            Debug.Log(label);

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
}
