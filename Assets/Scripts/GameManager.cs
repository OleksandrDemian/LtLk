using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class GameManager : MonoBehaviour, IEventListener
{
    public static GameManager Instance
    {
        get;
        private set;
    }

    private MapManager map;
    private EventManager eventsManager;
    private List<Entity> entities;

    private void Awake()
    {
        Instance = this;
        map = new MapManager(15, 15);
        entities = new List<Entity>();
    }

	private void Start ()
    {
        map.GenerateMap();
        eventsManager = new EventManager();
        eventsManager.SetListener(this);
    }
	
	private void Update ()
    {
		
	}

    public void EndTurn()
    {
        Player.Instance.EnableMovement(false);
        //InformationWindow.ShowInformation("Test", "Turn ended", false);

        for (int i = 0; i < entities.Count; i++)
        {
            entities[i].OnTurnEnd();
        }

        eventsManager.Next();
    }

    public void AddEntity(Entity entity)
    {
        entities.Add(entity);
    }

    public void RemoveEntity(Entity entity)
    {
        entities.Remove(entity);
    }

    public void PlayerEvent(PlayerEvents type)
    {
        switch (type)
        {
            case PlayerEvents.ENDTURN:
                EndTurn();
                break;
            case PlayerEvents.DEAD:
                InformationWindow.ShowInformation("Game over", "Your hero is dead!");
                break;
        }
    }

    public void OnEventsEnd()
    {
        eventsManager.ResetEvents();
        Player.Instance.EnableMovement(true);
    }
}
