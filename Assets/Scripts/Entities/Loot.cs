using UnityEngine;

public class Loot : Entity
{
    private static int probability = 70;

    public override void Turn()
    {
        GameManager.Instance.OnEntityTurnEnd(this);
    }

    public override void Interact(Entity actor)
    {
        if (!(actor is Character))
            throw new System.Exception("There is something wrong: not a character is going to interact with a loot!");

        Character cActor = actor as Character;

        string msg = cActor.name + " get:\n";

        int gold = Random.Range(10, 20);
        int hPotions = Random.Range(0, 100);
        int sPotions = Random.Range(0, 100);
        int equipment = Random.Range(0, 100);
        
        msg += "Gold: " + gold + "\n";
        cActor.GetGold().AddQty(gold);

        if (hPotions > probability)
        {
            msg += "Health potion\n";
            cActor.GetHealthPotions().Increase();
        }

        if (sPotions > probability)
        {
            msg += "Stamina potion\n";
            cActor.GetStaminaPotions().Increase();
        }

        if (equipment > probability)
        {
            int amount = Random.Range(1, 4);
            Attribute damage = cActor.GetDamage();
            damage.IncreaseMaxValue(amount);
            damage.ResetValue();
            msg += "Better equipment (+" + amount + " to damage)";
        }

        InformationWindow.ShowInformation("Loot", msg);
        DisableEntity();
    }

    public override void ShowInfo()
    {
        InformationWindow.ShowInformation("Loot", "This is a loot", false, "entityinfo");
    }
}