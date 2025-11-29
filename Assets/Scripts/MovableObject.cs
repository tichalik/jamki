using System;
using Unity.VisualScripting;
using UnityEngine;

public class MovableObject : MonoBehaviour, IInteractible
{
    public bool CanInteract(Player2DController controller)
    {
        return Timer.Instance.Stage == Timer.AgeStage.Adult && controller.Item == null;
    }

    public void Interact(Player2DController controller)
    {
        if (controller.Item == null) //Pick up
        {
            controller.Item = this;

            var x = (transform.position - controller.transform.position);
            x = x.normalized * 0.5f;

            transform.SetParent(controller.transform);
            transform.localPosition = x;
        }
        else //Drop
        {
            Drop(controller);
        }
    }

    private void Drop(Player2DController controller)
    {
        controller.Item = null;
        transform.SetParent(null);
    }

    public void OnAged(Player2DController controller)
    {
        if (Timer.Instance.Stage != Timer.AgeStage.Adult)
        {
            Drop(controller);
        }
    }
}