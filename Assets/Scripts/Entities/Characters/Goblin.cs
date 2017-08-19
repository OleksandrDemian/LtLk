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

    public override void ApplyDamage(int amount, Character actor)
    {
        if(!Success(avoidDamageProbability, name + " avoids damage"))
            base.ApplyDamage(amount, actor);
    }
}