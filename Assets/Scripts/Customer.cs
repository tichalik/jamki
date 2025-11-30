using UnityEngine;

public class Customer : Interactible
{
    public override bool CanInteract(Player2DController controller)
    {
        return RupiecUI.Instance.hasRupiec;
    }

    public override void Interact(Player2DController controller)
    {
        GameManager.EndGame();
    }

    public override void OnAged(Player2DController controller)
    {
    }
}
