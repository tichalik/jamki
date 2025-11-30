using UnityEngine;

public abstract class Interactible:MonoBehaviour
{
    [SerializeField] private GameObject _interactionToken;

    public abstract bool CanInteract(Player2DController controller);
    public abstract void Interact(Player2DController controller);
    public abstract void OnAged(Player2DController controller);

    public void Show()
    {
        _interactionToken.SetActive(true);
    }

    public void Hide()
    {
        _interactionToken.SetActive(false);
    }
}
