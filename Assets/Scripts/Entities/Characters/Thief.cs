/// <summary>
/// Thief
/// Abilities:
/// Stealth
/// Steels gold
/// Avoid damage
/// Find gold on map
/// Contrattack
/// </summary>
public class Thief : Character
{
    private int steelGoldProbability = 20;
    private int stealthProbability = 50;
    private int avoidDamageProbability = 30;
    private int findGoldOnMapProbability = 20;
    private int contrAttackProbability = 30;

    public override void InitializeAttributes()
    {
        name = "Thief";
        SetDamage(7);
        SetHealth(20);
        SetStamina(20);
    }

    public override void ApplyDamage(int amount, Character actor)
    {
        bool success = Success(avoidDamageProbability, name + " avoids damage");
        if (!success)
            base.ApplyDamage(amount, actor);
        
        if (Success(contrAttackProbability, name + " contrattacks"))
            actor.ApplyDamage(CalculateDamage(), this);

        if (Success(steelGoldProbability, name + " steals gold"))
        {
            int qty = UnityEngine.Random.Range(0, 4);
            Item gold = actor.GetInventory().GetItem("Gold");
            gold.GetQty(qty);
        }
    }
    /*
    public override void OnBattleTurn(int turnIndex, Character enemy)
    {
        base.OnBattleTurn(turnIndex, enemy);

        if (Success(steelGoldProbability))
        {
            int qty = UnityEngine.Random.Range(0, 4);
            enemy.GetController().GetGold().AddQty(-qty);
            GetController().GetGold().AddQty(qty);
        }

        if (Success(contrAttackProbability))
            enemy.ApplyDamage(CalculateDamage());
    }
    */
    protected override void OnCharacterMoved()
    {
        base.OnCharacterMoved();
        if (Success(findGoldOnMapProbability, name + " finds gold on map"))
        {
            int gold = UnityEngine.Random.Range(2, 5);
            
            //Double gold near cities etc
            if (HasAsNearby(typeof(Building)))
                gold *= 2;

            Toast.ShowToast("You have found " + gold + " gold!", 1);
            inventory.GetItem("Gold").AddQty(gold);
        }
    }

    public override bool IsStealthy()
    {
        return Success(stealthProbability);
    }

    public override string GetDescription()
    {
        return "Abilities: Stealth, steels gold during battle, avoid damage, Find's gold on map";
    }
}