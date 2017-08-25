/// <summary>
/// Barbarian
/// Abilities:
/// Rigenerate stamina when attacked
/// Don't lose stamina during battle
/// Decrease enemy stamina
/// Do not consume stamina while moving on fields
/// </summary>
public class Barbarian : Character
{
    private int restoreStaminaProbability = 20;
    private int decreaseEnemyStaminaProbability = 15;

    public override void InitializeAttributes()
    {
        SetHealth(45);
        SetStamina(15);
        SetDamage(9);
        name = "Barbarian";
    }

    public override void ApplyDamage(int amount, Character actor)
    {        
        if (Success(decreaseEnemyStaminaProbability, name + " decreases enemy stamina"))
        {
            actor.GetStamina().Value--;
        }

        if (Success(restoreStaminaProbability, name + " restores stamina"))
        {
            stamina.Value++;
        }

        base.ApplyDamage(amount, actor);
    }

    protected override void OnAttackDone(Character victim)
    {
        //Does not lose stamina when attacks
        //base.OnAttackDone(victim);
    }
    /*
    public override void OnBattleTurn(int turnIndex, Character enemy)
    {
        //base.OnBattleTurn(turnIndex);
        if (Success(restoreStaminaProbability))
        {
            stamina.Value++;
        }

        if (Success(decreaseEnemyStaminaProbability))
        {
            enemy.GetStamina().Value--;
        }
    }
    */
    protected override void OnCharacterMoved()
    {
        //Does not lose stamina when on field
        if(currentNode.GetLandScape() != LandscapeType.FIELD)
            base.OnCharacterMoved();
    }

    public override string GetDescription()
    {
        return "Abilities: Rigenerate stamina during battle, don't lose stamina during battle, decrease enemy stamina, do not consume stamina while moving on filed landscape";
    }
}