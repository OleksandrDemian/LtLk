using UnityEngine;

public class Camp : Entity
{
    [SerializeField]
    private string[] enemies;
    [SerializeField]
    private int generationRate = 5;

    public override void Interact(Entity actor)
    {
        if (IsPlayer(actor))
            DisableEntity();
    }

    public override void ShowInfo()
    {
        InformationWindow.ShowInformation("Camp", "Here is were criminals are enrolled!");
    }

    public override void Turn()
    {
        GameManager manager = GameManager.Instance;

        if (manager.GetCurrentTurn() % generationRate == 0)
            GenerateEnemy();

        manager.OnEntityTurnEnd(this);
    }

    private void GenerateEnemy()
    {
        MapManager map = MapManager.Instance;
        for (int y = Y - 1; y < Y + 2; y++)
        {
            for (int x = X - 1; x < X + 2; x++)
            {
                Node node = map.GetNode(x, y);
                if (!node.HasEntity())
                {
                    int rand = Random.Range(0, enemies.Length);
                    string enemyName = enemies[rand];
                    GameObject enemy = ObjectPool.Get(enemyName);
                    enemy.GetComponent<Entity>().Initialize(x, y);
                    return;
                }
            }
        }
    }
}
