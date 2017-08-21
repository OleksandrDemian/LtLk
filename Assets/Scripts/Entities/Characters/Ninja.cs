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
    private int stealthProbability = 80;

    public override void InitializeAttributes()
    {
        SetHealth(30);
        SetStamina(18);
        SetDamage(10);
        name = "Ninja";
    }

    public override void ApplyDamage(int amount, Character actor)
    {
        bool success = Success(avoidDamageProbability, name + " avoids damage");
        if(!success)
            base.ApplyDamage(amount, actor);
    }

    public override int CalculateDamage()
    {
        bool success = Success(doubleDamageProbability, name + " double damage");

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
