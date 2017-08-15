/// <summary>
/// Thief
/// Abilities:
/// Stealth
/// Steels gold
/// Avoid damage
/// </summary>
public class Thief : Character
{
    private int steelGoldProbability = 20;
    private int stealthProbability = 50;
    private int avoidDamageProbability = 10;

    protected override void InitializeAttributes()
    {
        name = "Thief";
        SetDamage(10);
        SetHealth(10);
        SetStamina(20);
    }

    public override void ApplyDamage(int amount)
    {
        bool success = Success(avoidDamageProbability);
        if (!success)
            base.ApplyDamage(amount);
    }

    public override void OnBattleTurn(int turnIndex, Character enemy)
    {
        base.OnBattleTurn(turnIndex, enemy);

        if (Success(steelGoldProbability))
        {
            int qty = UnityEngine.Random.Range(0, 4);
            enemy.GetController().GetGold().AddQty(-qty);
            GetController().GetGold().AddQty(qty);
        }
    }

    public override bool IsStealthy()
    {
        return Success(stealthProbability);
    }

    public override string GetDescription()
    {
        return "Abilities: Stealth, steels gold during battle, avoid damage";
    }
}