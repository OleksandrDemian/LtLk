using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected Node currentNode;

    protected virtual void Start()
    {
        currentNode = MapManager.Instance.GetNode(X, Y);
        currentNode.SetEntity(this);
        GameManager.Instance.AddEntity(this);
    }

    protected virtual void OnDisable()
    {
        //Debug.Log("Diasable: " + name);
        DisableEntity();
    }

    protected void DisableEntity()
    {
        currentNode.ReleaseEntity();
        GameManager.Instance.RemoveEntity(this);
    }

    public virtual void OnTurnEnd()
    {
        Debug.Log(name + " onTurnEnd!");
    }

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

    protected abstract void Update();
    public abstract void Interact(Entity actor);
}
