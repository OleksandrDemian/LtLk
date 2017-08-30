/// <summary>
/// Skeleton
/// Abilities:
/// Resurection(if there are friendly necromancers nearby
/// Restores 2 points of his health and necromancer's health each turn if there are friendly necromancers nearby
/// </summary>
public class Skeleton : Character
{
    private int resurectionProbability = 20;

    public override void InitializeAttributes()
    {
        name = "Skeleton";
        SetDamage(5);
        SetHealth(15);
        SetStamina(15);
    }

    protected override bool OnCharacterDeath()
    {
        Necromancer necr = GetNearby<Necromancer>() as Necromancer;
        if (necr != null)
        {
            if (necr.IsPlayer() == IsPlayer())
                return Success(resurectionProbability, name + " resurected by a nearby necromancer");
        }
        return base.OnCharacterDeath();
    }

    public override void OnTurnStart()
    {
        Necromancer necr = GetNearby<Necromancer>() as Necromancer;
        if (necr != null)
        {
            if (necr.IsPlayer() == IsPlayer())
            {
                health.Value += 2;
                necr.GetHealth().Value += 2;
            }
        }
    }
}