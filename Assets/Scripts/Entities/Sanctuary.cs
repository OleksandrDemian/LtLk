public class Sanctuary : Entity
{
    public override void Turn()
    {
        GameManager.Instance.OnEntityTurnEnd(this);
    }

    public override void Interact(Entity actor)
    {
        Character c = actor as Character;
        if (c == null)
            return;
        Player con = c.GetController() as Player;
        if (con == null)
            return;

        Choice[] choices = new Choice[4];
        choices[0] = new Choice("Offer 10", delegate()
        {
            Pray(10, con);
        });
        choices[1] = new Choice("Offer 50", delegate ()
        {
            Pray(50, con);
        });
        choices[2] = new Choice("Offer 100", delegate ()
        {
            Pray(100, con);
        });
        choices[3] = new Choice("Offer 500", delegate ()
        {
            Pray(500, con);
        });
        ChoiceWindow.Open("Sanctuary", "You are in front of a sanctuary. Try to donnate something and maybe gods will give you something back", choices);
    }

    private void Pray(int gold, Player p)
    {
        if (p.GetGold().GetQty() < gold)
        {
            InformationWindow.ShowInformation("No money", "You are to poor!");
            return;
        }

        p.AddGold(-gold);

        int min = UnityEngine.Random.Range(0, 600);
        if (gold > min)
        {
            p.Training(5);
        }
    }

    public override void ShowInfo()
    {
        InformationWindow.ShowInformation("Sanctuary", "This is a sanctuary", false, "entityinfo");
    }
}
