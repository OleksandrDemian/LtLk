using UnityEngine;

public class Enemy : MCharacterController
{
    [SerializeField]
    private bool isAggressive = true;

    public override void Initialize(Character character)
    {
        base.Initialize(character);
        character.SetIsPlayer(false);
        Item gold = new Item("Gold", 20);
        character.GetInventory().AddItem(gold, true);
    }

    public override void OnCharacterDead()
    {
        base.OnCharacterDead();
    }

    public override bool StartTurn()
    {
        if (character.GetStamina().Value < 1)
            return false;

        return true;
    }

    public override void TurnUpdate()
    {
        if (StartTurn())
        {
            character.OnTurnStart();
            CheckPlayer();
        }
        EndTurn();
    }

    protected override void EndTurn()
    {
        if (!character.DidSomeAction)
        {
            Sleep();
        }

        if (!isAnimated)
            ControllerEndTurn();
    }

    private void Sleep()
    {
        character.RestoreStamina(4);
        character.RestoreHealth(1);
    }

    private bool CheckPlayer()
    {
        if (!isAggressive)
            return false;
        
        Character player = Player.Instance.GetCharacter();
        
        int distance = character.Distance(player);

        if (distance < 3)
        {
            //If target is not directly near, check if you can see it
            if (distance > 1)
            {
                if (player.IsStealthy())
                    return false;
            }
            
            Vector3 dir = player.transform.position - transform.position;

            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.z))
            {
                int direction = dir.x > 0 ? 1 : -1;
                character.MoveDirection(direction, 0);
            }
            else
            {
                int direction = dir.z > 0 ? 1 : -1;
                character.MoveDirection(0, direction);
            }
            return true;
        }
        return false;
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
