public class Node
{
    private bool isWalkable;
    private Entity entity;

    public Node(bool isWalkable)
    {
        this.isWalkable = isWalkable;
        entity = null;
    }

    public bool IsWalkable
    {
        get
        {
            return isWalkable;
        }
    }

    public bool HasEntity()
    {
        return entity != null;
    }

    public Entity GetEntity()
    {
        return entity;
    }

    public void ReleaseEntity()
    {
        entity = null;
    }

    public void SetEntity(Entity entity)
    {
        this.entity = entity;
    }
}