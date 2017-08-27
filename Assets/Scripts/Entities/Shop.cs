using UnityEngine;

public class Shop : Entity
{

    [SerializeField]
    private int healthPotionCost = 10;
    [SerializeField]
    private int staminaPotionCost = 10;
    [SerializeField]
    private int equipmentCost = 12;
    [SerializeField]
    private int equipmentQuality = 2;

    public override void Turn()
    {
        GameManager.Instance.OnEntityTurnEnd(this);
    }

    public override void Interact(Entity actor)
    {
        if (!IsPlayer(actor))
            return;

        Choice[] choices = new Choice[4];

        choices[0] = new Choice("Buy health potion (" + healthPotionCost + " gold)", delegate()
        {
            Inventory i = Player.Instance.GetCharacter().GetInventory();
            Item gold = Player.Instance.GetCharacter().GetGold();
            if (gold.Get(healthPotionCost))
            {
                i.AddItem(new Item("Health potion", 1));
            }
            else
            {
                InformationWindow.ShowInformation("No money", "You are to poor to pay for health potion!", false);
            }
        });

        choices[1] = new Choice("Buy stamina potion (" + staminaPotionCost + " gold)", delegate()
        {
            Inventory i = Player.Instance.GetCharacter().GetInventory();
            Item gold = Player.Instance.GetCharacter().GetGold();
            if (gold.Get(staminaPotionCost))
            {
                i.AddItem(new Item("Stamina potion", 1));
            }
            else
            {
                InformationWindow.ShowInformation("No money", "You are to poor to pay for stamina potion!", false);
            }
        });

        choices[2] = new Choice("Buy better equipment (" + equipmentCost + " gold)", delegate ()
        {
            Item gold = Player.Instance.GetCharacter().GetGold();
            if (gold.Get(equipmentCost))
            {
                Attribute damage = Player.Instance.GetCharacter().GetDamage();
                damage.IncreaseDefaultValue(equipmentQuality);
                damage.ResetValue();
            }
            else
            {
                InformationWindow.ShowInformation("No money", "You are to poor to pay for new equipment potion!", false);
            }
        });

        choices[3] = new Choice("Go away", null);

        ChoiceWindow.Open("City", "You are in the city of Patrunacs", choices);
    }

    public override void ShowInfo()
    {
        InformationWindow.ShowInformation("Building", "This is building", false, "entityinfo");
    }
}