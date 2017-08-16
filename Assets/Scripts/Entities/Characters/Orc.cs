/// <summary>
/// Orc
/// Abilities:
/// </summary>
public class Orc : Character
{
    public override void InitializeAttributes()
    {
        SetDamage(8);
        SetHealth(30);
        SetStamina(15);
        name = "Orc";
    }
}