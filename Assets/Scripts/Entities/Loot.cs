using UnityEngine;

public class Loot : Entity
{
    private static int probability = 70;

    public override void Interact(Entity actor)
    {
        string msg = "You get:\n";

        int gold = Random.Range(10, 20);
        int hPotions = Random.Range(0, 100);
        int sPotions = Random.Range(0, 100);
        int equipment = Random.Range(0, 100);

        Player p = Player.Instance;

        msg += "Gold: " + gold + "\n";
        p.AddGold(gold);

        if (hPotions > probability)
        {
            msg += "Health potion\n";
            p.AddHealthPotion(1);
        }

        if (sPotions > probability)
        {
            msg += "Stamina potion\n";
            p.AddStaminaPotion(1);
        }

        if (equipment > probability)
        {
            int amount = Random.Range(1, 4);
            Attribute damage = p.GetCharacter().GetDamage();
            damage.IncreaseDefaultValue(amount);
            damage.ResetValue();
            msg += "Better equipment (+" + amount + " to damage)";
        }

        InformationWindow.ShowInformation("Loot", msg);
        gameObject.SetActive(false);
    }

    public override void ShowInfo()
    {
        InformationWindow.ShowInformation("Loot", "This is a loot", false, "entityinfo");
    }
}