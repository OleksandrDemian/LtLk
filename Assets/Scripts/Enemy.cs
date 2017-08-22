using UnityEngine;

public class Enemy : MCharacterController
{
    [SerializeField]
    private bool isAggressive = true;

    protected override void Awake()
    {
        base.Awake();
        character.SetIsPlayer(false);
    }

    public override void CharacterStateListener(CharacterEvents cEvent)
    {
        switch (cEvent)
        {
            default:
                Debug.Log(name + " event: " + cEvent);
                break;
        }
    }

    public override bool StartTurn()
    {
        return true;
    }

    public override void TurnUpdate()
    {
        bool endTurn = CheckPlayer();
        EndTurn();
    }

    private void EndTurn()
    {
        if (!character.DidSomeAction)
        {
            Sleep();
        }

        character.EndTurn(true);
    }

    private void Sleep()
    {
        character.GetStamina().Value += 4;
        character.GetHealth().Value++;
    }

    private bool CheckPlayer()
    {
        if (!isAggressive)
            return true;
        
        Character player = Player.Instance.GetCharacter();
        
        int distance = character.Distance(player);

        if (distance < 3)
        {
            //If target is not directly near, check if you can see it
            if (distance > 1)
            {
                if (player.IsStealthy())
                    return true;
            }
            
            Vector3 dir = player.transform.position - transform.position;

            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {
                int direction = dir.x > 0 ? 1 : -1;
                character.MoveDirection(direction, 0);
            }
            else
            {
                int direction = dir.y > 0 ? 1 : -1;
                character.MoveDirection(0, direction);
            }
            return true;
        }
        return true;
    }

    public override bool InteractWith(Entity target)
    {
        if (target is Character)
        {
            Character c = target as Character;
            if (c.IsPlayer)
            {
                character.Attack(c);
            }
        }
        else
        {
            target.Interact(character);
        }
        return true;
    }

    public override Item GetGold()
    {
        return new Item("Gold", 50);
    }

    #region AttributesValueHandlers

    public override void OnDamageValueChange(int value, int oldValue)
    {

    }

    public override void OnHealthValueChange(int value, int oldValue)
    {

    }

    public override void OnStaminaValueChange(int value, int oldValue)
    {

    }

    #endregion
}
