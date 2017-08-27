public enum LandscapeType
{
    FIELD,
    MOUNTAIN,
    FOREST,
    WATTER
}

public class Node
{
    private Entity entity;
    private LandscapeType landscape;

    public Node()
    {
        entity = null;
    }

    public Node(LandscapeType type)
    {
        landscape = type;
        entity = null;
    }

    public bool IsWalkable
    {
        get
        {
            return landscape == LandscapeType.MOUNTAIN ? false : true;
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