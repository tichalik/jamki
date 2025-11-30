using UnityEngine;

public class Customer : MonoBehaviour, IInteractible
{
    public bool CanInteract(Player2DController controller)
    {
        return true; //Ma rupiecia
    }

    public void Interact(Player2DController controller)
    {
        GameManager.EndGame();
    }

    public void OnAged(Player2DController controller)
    {
    }
}
