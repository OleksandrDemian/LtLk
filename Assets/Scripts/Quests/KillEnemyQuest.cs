using UnityEngine;

//[CreateAssetMenu(fileName = "KillEnemyQuest", menuName = "Quest/Kill enemy quest")]
public class KillEnemyQuest : Quest
{
    [SerializeField]
    private Character target;

    public override bool Check()
    {
        return !target.IsAlive();
    }

    public override void Initialize()
    {
        target.onCharacterDeath += OnQuestDone;
    }
}