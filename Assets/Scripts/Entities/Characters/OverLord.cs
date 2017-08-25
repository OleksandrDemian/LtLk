public class OverLord : Character
{
    private int resurectionProbability = 45;

    public override void InitializeAttributes()
    {
        SetDamage(20);
        SetHealth(100);
        SetStamina(50);
        name = "OverLord";
    }

    public override void ApplyDamage(int amount, Character actor)
    {
        amount = amount / 2;
        actor.ApplyDamage(CalculateDamage(), this);
        base.ApplyDamage(amount, actor);
    }

    public override int CalculateDamage()
    {
        return damage.Value;
    }

    protected override void OnAttackDone(Character victim)
    {
        
    }

    protected override void OnCharacterMoved()
    {
        
    }

    public override string GetDescription()
    {
        return "The supreme lord of the world";
    }

    public override void RestoreHealth(int amount)
    {
        health.ResetValue();
    }

    public override void RestoreStamina(int amount)
    {
        stamina.ResetValue();
    }

    protected override bool OnCharacterDeath()
    {
        if (Success(resurectionProbability, name + " resurected after death!"))
        {
            health.ResetValue();
            stamina.ResetValue();
            return false;
        }

        return true;
    }
}
