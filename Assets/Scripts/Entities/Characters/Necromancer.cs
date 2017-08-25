/// <summary>
/// Necromancer
/// Abilities>
/// Chance to create bone shield (each shield blocks completly damage)
/// Killing an enemy incremets it's damage by 2
/// Killing an enemy restores it's stamina
/// Damage does not depending of stamina
/// </summary>
public class Necromancer : Character
{
    private int createBoneShieldProbability = 25;
    private int damageModifier = 0;
    private int boneShields = 0;
    private int resurectionProbability = 30;

    public override void InitializeAttributes()
    {
        name = "Necromancer";
        SetDamage(10);
        SetHealth(15);
        SetStamina(10);
    }

    public override int CalculateDamage()
    {
        return damage.Value + damageModifier;
    }

    public override void ApplyDamage(int amount, Character actor)
    {
        if (boneShields > 0)
        {
            boneShields--;
            return;
        }

        base.ApplyDamage(amount, actor);
    }

    protected override void OnAttackDone(Character victim)
    {
        base.OnAttackDone(victim);

        //Creates bone shield
        if (Success(createBoneShieldProbability, name + " creates bone shield"))
            boneShields++;

        if (!victim.IsAlive())
        {
            //Restore stamina
            stamina.ResetValue();
            //Increment damage modifier
            damageModifier += 2;
        }
    }

    private void InteractWithSkeleton(Skeleton actor)
    {
        actor.gameObject.SetActive(false);
        //Restore stamina
        stamina.ResetValue();
        //Increment damage modifier
        damageModifier += 2;
        //Increment boneShields
        boneShields += 2;
        //Restore health
        health.ResetValue();
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

    public override string GetDescription()
    {
        return "Abilities: After killing enemy restores stamina and increases damage by two, chance to create bone shield that absorbs completly damage";
    }
}