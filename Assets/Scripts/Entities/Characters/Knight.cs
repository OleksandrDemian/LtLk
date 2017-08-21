/// <summary>
/// Knight
/// Abilities:
/// Horse: does not consume stamina while moving
/// Lose 2 stamina for attack
/// Lose 1 stamina while attacked
/// Probability to block attack
/// </summary>
public class Knight : Character
{
    private int blockAttackProbability = 40;

    public override void InitializeAttributes()
    {
        SetDamage(9);
        SetHealth(60);
        SetStamina(25);
        name = "Knight";
    }

    protected override void OnCharacterMoved()
    {
        //Does not lose stamina
    }

    protected override void OnAttackDone(Character victim)
    {
        stamina.Value -= 2;
    }

    public override void ApplyDamage(int amount, Character actor)
    {
        if(stamina.Value < 1)
            health.Value -= amount;
        else if (!Success(blockAttackProbability, name + " blocks attack"))
            health.Value -= amount;

        stamina.Value--;
    }
}