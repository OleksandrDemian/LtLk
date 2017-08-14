﻿using UnityEngine;

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
            if (p.GetGold() >= trainingGoldRequired)
            {
                int incHealth = Random.Range(0, 4);
                int incDamage = Random.Range(0, 2);
                int incStamina = Random.Range(0, 2);
                p.AddGold(-trainingGoldRequired);

                trainingGoldRequired += 20;

                string msg = "";

                if (incHealth > 0)
                {
                    msg += "Your health rised up by " + incHealth + " points!\n";
                    p.GetCharacter().GetHealth().IncreaseDefaultValue(incHealth);
                }
                if (incDamage > 0)
                {
                    msg += "Your damage rised up by " + incDamage + " points!\n";
                    p.GetCharacter().GetDamage().IncreaseDefaultValue(incDamage);
                }
                if (incStamina > 0)
                {
                    msg += "Your stamina rised up by " + incStamina + " points!";
                    p.GetCharacter().GetStamina().IncreaseDefaultValue(incStamina);
                }

                InformationWindow.ShowInformation("Train result", msg);
            }
            else
            {
                InformationWindow.ShowInformation("No money", "You are to poor to pay for training!");
            }
        });

        choices[2] = new Choice("Buy health potion (10 gold)", delegate()
        {
            Player p = Player.Instance;
            if (p.GetGold() >= 10)
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
            if (p.GetGold() >= 10)
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

    protected override void Update()
    {
        
    }
}