using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public static PlayerSpawner Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnPlayer()
    {
        string character = PlayerPrefs.GetString("character", "Ninja");
        GameObject player = ObjectPool.Get(character);
        player.transform.position = transform.position;
        player.AddComponent<Player>();
        Destroy(gameObject);
    }
}
