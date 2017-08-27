public class Sanctuary : Entity
{
    public override void Turn()
    {
        GameManager.Instance.OnEntityTurnEnd(this);
    }

    public override void Interact(Entity actor)
    {
        if (!IsPlayer(actor))
            return;

        Character c = actor as Character;

        Choice[] choices = new Choice[5];
        choices[0] = new Choice("Offer 10", delegate()
        {
            Pray(10, c);
        });
        choices[1] = new Choice("Offer 50", delegate ()
        {
            Pray(50, c);
        });
        choices[2] = new Choice("Offer 100", delegate ()
        {
            Pray(100, c);
        });
        choices[3] = new Choice("Offer 500", delegate ()
        {
            Pray(500, c);
        });

        choices[4] = new Choice("Go away", null);
        ChoiceWindow.Open("Sanctuary", "You are in front of a sanctuary. Try to donnate something and maybe gods will give you something back", choices);
    }

    private void Pray(int gold, Character actor)
    {
        if (!actor.GetGold().Get(gold))
        {
            InformationWindow.ShowInformation("No money", "You are to poor!");
            return;
        }

        int min = UnityEngine.Random.Range(0, 600);
        if (gold > min)
        {
            //Not sure about this decision
            Player.Instance.Training(5);
        }
    }

    public override void ShowInfo()
    {
        InformationWindow.ShowInformation("Sanctuary", "This is a sanctuary", false, "entityinfo");
    }
}
