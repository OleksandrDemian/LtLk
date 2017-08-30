public class Zombie : Character
{
    public override void InitializeAttributes()
    {
        SetDamage(8);
        SetHealth(30);
        SetStamina(15);
        name = "Zombie";
    }

    protected override void OnCharacterKilled(Character victim)
    {
        base.OnCharacterKilled(victim);
        health.IncreaseMaxValue(2);
        health.ResetValue();
    }
}