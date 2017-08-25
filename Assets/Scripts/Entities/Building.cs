public class Building : Entity
{
    private int trainingGoldRequired = 50;

    public override void Turn()
    {
        GameManager.Instance.OnEntityTurnEnd(this);
    }

    public override void Interact(Entity actor)
    {
        Choice[] choices = new Choice[5];
        choices[0] = new Choice("Say hello!", delegate()
        {
            InformationWindow.ShowInformation("City", "All the people are looking at you!");
        });

        choices[1] = new Choice("Training (" + trainingGoldRequired + " gold)", delegate ()
        {
            Player p = Player.Instance;
            Item gold = p.GetCharacter().GetGold();

            if (gold.Get(trainingGoldRequired))
            {
                p.Training(1);
                trainingGoldRequired += 20;
            }
            else
            {
                InformationWindow.ShowInformation("No money", "You are to poor to pay for training!");
            }
        });

        choices[2] = new Choice("Buy health potion (10 gold)", delegate()
        {
            Inventory i = Player.Instance.GetCharacter().GetInventory();
            Item gold = Player.Instance.GetCharacter().GetGold();
            if (gold.Get(10))
            {
                i.AddItem(new Item("Health potion", 1));
            }
            else
            {
                InformationWindow.ShowInformation("No money", "You are to poor to pay for health potion!");
            }
        });

        choices[3] = new Choice("Buy stamina potion (10 gold)", delegate()
        {
            Inventory i = Player.Instance.GetCharacter().GetInventory();
            Item gold = Player.Instance.GetCharacter().GetGold();
            if (gold.Get(10))
            {
                i.AddItem(new Item("Stamina potion", 1));
            }
            else
            {
                InformationWindow.ShowInformation("No money", "You are to poor to pay for stamina potion!");
            }
        });

        choices[4] = new Choice("Go away", null);

        ChoiceWindow.Open("City", "You are in the city of Patrunacs", choices);
    }

    public override void ShowInfo()
    {
        InformationWindow.ShowInformation("Building", "This is building", false, "entityinfo");
    }
}