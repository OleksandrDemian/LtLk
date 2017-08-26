public enum LandscapeType
{
    FIELD,
    MOUNTAIN,
    FOREST,
    WATTER
}

public class Node
{
    private bool isWalkable;
    private Entity entity;
    private LandscapeType landscape;

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

    public LandscapeType GetLandScape()
    {
        return landscape;
    }

    public void SetLandscape(LandscapeType landscape)
    {
        this.landscape = landscape;
    }
}