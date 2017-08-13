public delegate void BattleListener(int turn, string eventName, string description);

public class BattleManager
{
    private Character character1;
    private Character character2;

    private int turnCounter = 0;

    public BattleManager(Character character1, Character character2)
    {
        this.character1 = character1;
        this.character2 = character2;
    }

    public void StartBattle()
    {
        Attribute h1 = character1.GetHealth();
        Attribute h2 = character2.GetHealth();

        Attribute d1 = character1.GetDamage();
        Attribute d2 = character2.GetDamage();

        while (h1.Value > 0 && h2.Value > 0)
        {
            h1.Value -= d2.Value;
            h2.Value -= d1.Value;
        }

        if (h1.Value < 1)
        {
            InformationWindow.ShowInformation("Battle result", character2.name + " wins the battle!");
            character1.Death();
        }
        else if (h2.Value < 1)
        {
            InformationWindow.ShowInformation("Battle result", character1.name + " wins the battle!");
            character2.Death();
        }

        InformationWindow.ShowInformation("Result:", character1.name + " health: " + h1.Value + "\n" + character2.name + " health: " + h2.Value);
    }
}
