/// <summary>
/// Ninja
/// Abilities:
/// Avoid damage
/// Double damage
/// Stealth
/// </summary>
public class Ninja : Character
{
    private int avoidDamageProbability = 30;
    private int doubleDamageProbability = 15;
    private int stealthProbability = 90;
    
    public override void ApplyDamage(int amount)
    {
        bool success = Success(avoidDamageProbability);
        if(!success)
            base.ApplyDamage(amount);
    }

    public override int CalculateDamage()
    {
        bool success = Success(doubleDamageProbability);

        if (success)
            return base.CalculateDamage() * 2;

        return base.CalculateDamage();
    }

    public override bool IsStealthy()
    {
        return Success(stealthProbability);
    }
}
