using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class GameManager : MonoBehaviour, IEventListener
{
    [SerializeField]
    private Texture2D mapTexture;

    public static GameManager Instance
    {
        get;
        private set;
    }

    private MapManager map;
    private EventsManager eventsManager;
    private EntitiesManager entities;
    private int currentTurn = 1;

    private void Awake()
    {
        Instance = this;

        if (mapTexture == null)
            map = new MapManager(15, 15);
        else
            map = new MapManager(mapTexture);

        entities = new EntitiesManager();
    }

	private void Start ()
    {
        map.GenerateMap();
        eventsManager = new EventsManager();
        eventsManager.SetListener(this);

        PlayerSpawner.Instance.SpawnPlayer();

        LoadEntities();
        StartCoroutine(StartWait());
    }

    private void LoadEntities()
    {
        Entity[] entities = FindObjectsOfType<Entity>();
        for (int i = 0; i < entities.Length; i++)
        {
            this.entities.AddEntity(entities[i]);
            entities[i].Initialize();
        }
    }

    private IEnumerator StartWait()
    {
        for (int i = 0; i < 3; i++)
        {
            Toast.ShowToast("Wait: " + (3 - i), 1);
            yield return new WaitForSeconds(1);
        }
        HistoryTracer.Instance.AddToHistory("Game started");
        Quest.Instance.ShowQuest();
        entities.NextEntityUpdate();
    }

    public void OnEntityTurnEnd(Entity entity)
    {
        //entities.NextEntityUpdate();
        //StartCoroutine(NextEntity());
        eventsManager.Next();
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
        HistoryTracer.Instance.AddToHistory("Turn " + currentTurn + " ends!");
        currentTurn++;
        eventsManager.Next();
    }

    public void OnEventsEnd()
    {
        eventsManager.ResetEvents();
        StartCoroutine(NextEntity());
    }

    public int GetCurrentTurn()
    {
        return currentTurn;
    }
}
