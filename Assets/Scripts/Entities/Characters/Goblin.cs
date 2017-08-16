/// <summary>
/// Goblin
/// Abilities:
/// Avoid damage
/// </summary>
public class Goblin : Character
{
    private int avoidDamageProbability = 35;

    public override void InitializeAttributes()
    {
        SetDamage(2);
        SetHealth(15);
        SetStamina(15);
        name = "Goblin";
    }

    public override void ApplyDamage(int amount)
    {
        if(!Success(avoidDamageProbability))
            base.ApplyDamage(amount);
    }
}