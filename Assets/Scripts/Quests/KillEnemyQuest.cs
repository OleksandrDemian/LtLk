using UnityEngine;

public class KillEnemyQuest : Quest
{
    [SerializeField]
    private GameObject target;

    private void Start()
    {
        ObjectPool.Instance.onObjectDisabled += Listener;
    }

    private void Listener(GameObject obj)
    {
        if (target == obj)
        {
            InformationWindow.ShowInformation("Quest", title + " done!", false);
            done = true;
            ObjectPool.Instance.onObjectDisabled -= Listener;
        }
    }
}