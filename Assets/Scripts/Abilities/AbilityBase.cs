public enum AbilityUsage
{
    ATTACK,
    DEFENCE
}

public abstract class AbilityBase
{
    private AbilityUsage usage;

    public abstract void Trigger();
}
