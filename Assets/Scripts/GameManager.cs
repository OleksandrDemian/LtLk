using System.Collections;
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
    private EntitiesManager entities;

    private void Awake()
    {
        Instance = this;
        map = new MapManager(15, 15);
        entities = new EntitiesManager();
    }

	private void Start ()
    {
        map.GenerateMap();
        eventsManager = new EventManager();
        eventsManager.SetListener(this);
        Debug.Log("GM: Start");
        StartCoroutine(StartWait());
    }

    private IEnumerator StartWait()
    {
        Debug.Log("Start wait!");
        yield return new WaitForSeconds(1);
        Debug.Log("Game starts!");
        entities.NextEntityUpdate();
    }

    public void OnEntityTurnEnd(Entity entity)
    {
        //entities.NextEntityUpdate();
        StartCoroutine(NextEntity());
    }

    private IEnumerator NextEntity()
    {
        yield return null;
        entities.NextEntityUpdate();
    }

    public void PlayerEvent(PlayerEvents type)
    {
        switch (type)
        {
            case PlayerEvents.DEAD:
                InformationWindow.ShowInformation("Game over", "Your hero is dead!");
                break;
        }
    }

    public void TurnEnd()
    {
        eventsManager.Next();
    }

    public void OnEventsEnd()
    {
        eventsManager.ResetEvents();
        //Player.Instance.EnableMovement(true);
        entities.NextEntityUpdate();
    }
}
