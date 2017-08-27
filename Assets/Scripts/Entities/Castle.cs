using UnityEngine;

public class Castle : Entity
{
    [SerializeField]
    private int trainingGoldRequired = 50;

    [SerializeField]
    private int restGoldRequired = 10;

    public override void Interact(Entity actor)
    {
        if (!IsPlayer(actor))
            return;

        Choice[] choices = new Choice[3];

        choices[0] = new Choice("Training (" + trainingGoldRequired + " gold)", delegate ()
        {
            Player p = Player.Instance;
            Item gold = p.GetCharacter().GetGold();

            if (gold.Get(trainingGoldRequired))
            {
                p.Training(1);
                trainingGoldRequired += 10;
            }
            else
            {
                InformationWindow.ShowInformation("No money", "You are to poor to pay for training!", false);
            }
        });

        choices[1] = new Choice("Rest (" + restGoldRequired + " gold)", delegate ()
        {
            Player p = Player.Instance;
            Item gold = p.GetCharacter().GetGold();

            if (gold.Get(restGoldRequired))
            {
                p.GetCharacter().GetHealth().ResetValue();
                p.GetCharacter().GetStamina().ResetValue();
            }
            else
            {
                InformationWindow.ShowInformation("No money", "You are to poor to pay for training!", false);
            }
        });

        choices[2] = new Choice("Go away", null);

        ChoiceWindow.Open("Castle", ToString(), choices);
    }

    public override void ShowInfo()
    {
        InformationWindow.ShowInformation("Castle", ToString());
    }

    public override void Turn()
    {
        GameManager.Instance.OnEntityTurnEnd(this);
    }

    public override string ToString()
    {
        return "This is a castle, here you can train yourself or rest for a while";
    }
}