/// <summary>
/// Barbarian
/// Abilities:
/// Rigenerate stamina
/// Don't lose stamina during battle
/// Decrease enemy stamina
/// </summary>
public class Barbarian : Character
{
    private int restoreStaminaProbability = 20;
    private int decreaseEnemyStaminaProbability = 15;

    protected override void InitializeAttributes()
    {
        SetHealth(30);
        SetStamina(15);
        SetDamage(17);
        name = "Barbarian";
    }

    public override void ApplyDamage(int amount)
    {
        base.ApplyDamage(amount);
    }

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

    public override string GetDescription()
    {
        return "Abilities: Rigenerate stamina during battle, don't lose stamina during battle, decrease enemy stamina";
    }
}