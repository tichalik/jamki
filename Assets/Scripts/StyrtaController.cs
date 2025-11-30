using UnityEngine;

public class StyrtaController : Interactible
{
    [SerializeField] private Rupiec _rupiec;

    private void OnEnable()
    {
        Character.OnAgeChanged += HandleAgeChange;
    }

    private void OnDisable()
    {
        Character.OnAgeChanged -= HandleAgeChange;
    }

    private void HandleAgeChange(int ageIndex)
    {
        if (_rupiec == null) return;

        Timer.AgeStage state = (Timer.AgeStage)ageIndex;

        if (state == Timer.AgeStage.Dziad)
        {
            _rupiec.gameObject.SetActive(true);
        }
        else
        {
            _rupiec.gameObject.SetActive(false);
        }
    }

    public override bool CanInteract(Player2DController controller)
    {
        return Timer.Instance.Stage == Timer.AgeStage.Dziad && _rupiec != null;
    }

    public override void Interact(Player2DController controller)
    {
        Destroy(_rupiec.gameObject);
        _rupiec = null;
        RupiecUI.Instance.setRupiecFoundColor();
    }

    public override void OnAged(Player2DController controller)
    {
    }
}
