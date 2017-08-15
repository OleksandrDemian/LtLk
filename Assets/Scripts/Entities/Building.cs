using UnityEngine;

public class Building : Entity
{
    private int trainingGoldRequired = 50;

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
            if (p.GetGold().GetQty() >= trainingGoldRequired)
            {
                p.AddGold(-trainingGoldRequired);
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
            Player p = Player.Instance;
            if (p.GetGold().GetQty() >= 10)
            {
                p.AddGold(-10);
                p.AddHealthPotion(1);
            }
            else
            {
                InformationWindow.ShowInformation("No money", "You are to poor to pay for health potion!");
            }
        });

        choices[3] = new Choice("Buy stamina potion (10 gold)", delegate()
        {
            Player p = Player.Instance;
            if (p.GetGold().GetQty() >= 10)
            {
                p.AddGold(-10);
                p.AddStaminaPotion(1);
            }
            else
            {
                InformationWindow.ShowInformation("No money", "You are to poor to pay for stamina potion!");
            }
        });

        choices[4] = new Choice("Go away", null);

        ChoiceWindow.Open("City", "You are in the city of Patrunacs", choices);
    }
}