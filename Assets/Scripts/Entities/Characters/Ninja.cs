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

    protected override void InitializeAttributes()
    {
        SetHealth(20);
        SetStamina(20);
        SetDamage(20);
        name = "Ninja";
    }

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

    public override string GetDescription()
    {
        return "Abilities: Avoid damage, Double damage, Stealth";
    }
}
