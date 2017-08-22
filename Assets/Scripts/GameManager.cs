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
        //StartCoroutine(StartWait());
        //entities.NextEntityUpdate();
        LoadEntities();
        StartCoroutine(StartWait());
    }

    private void LoadEntities()
    {
        Entity[] entities = FindObjectsOfType<Entity>();
        for (int i = 0; i < entities.Length; i++)
        {
            this.entities.AddEntity(entities[i]);
            entities[i].OnGameStart();
        }
    }

    private IEnumerator StartWait()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1);
            Debug.Log("Wait: " + (3 - i));
            Toast.ShowToast("Wait: " + (3 - i), 1);
        }
        Debug.Log("Game starts");
        entities.NextEntityUpdate();
    }

    public void OnEntityTurnEnd(Entity entity)
    {
        //entities.NextEntityUpdate();
        StartCoroutine(NextEntity());
    }

    private IEnumerator NextEntity()
    {
        yield return new WaitForSeconds(.1f);
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
