using UnityEngine;

public interface IInteractible
{
    bool CanInteract(Player2DController controller);
    void Interact(Player2DController controller);
    void OnAged(Player2DController controller);
}
