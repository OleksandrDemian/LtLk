using UnityEngine;

public class Sanctuary : Entity
{
    public override void Interact(Entity actor)
    {
        InformationWindow.ShowInformation("Sanctuary", actor.name + " enters to sanctuary");
    }

    protected override void Update()
    {

    }
}
