using System.Collections.Generic;

public class EntitiesManager
{
    private List<Entity> entities;
    private int currentEntity = 0;

    public static EntitiesManager Instance
    {
        get;
        private set;
    }

    public EntitiesManager()
    {
        Instance = this;
        entities = new List<Entity>();
    }

    public void AddEntity(Entity entity)
    {
        entities.Add(entity);
    }

    public void RemoveEntity(Entity entity)
    {
        entities.Remove(entity);
    }

    //Problem: if I remove entity after turn, currentEntity will be wrong(maybe it will give an error)
    public void NextEntityUpdate()
    {
        if (currentEntity >= entities.Count)
        {
            currentEntity = 0;
            GameManager.Instance.TurnEnd();
            return;
        }

        //UnityEngine.Debug.Log("Update: " + entities[currentEntity].name);
        entities[currentEntity].Turn();
        currentEntity++;
    }
}
