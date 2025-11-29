using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class LayeredObject : MonoBehaviour
{
    private SpriteRenderer boxRenderer;

    private void Awake()
    {
        boxRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Character.OnMoved += OnMoved;
    }

    private void OnDisable()
    {
        Character.OnMoved -= OnMoved;
    }

    private void OnMoved(Transform playerT)
    {
        var vec2player = transform.position - playerT.position;
        if (vec2player.y > 0)
        {
            boxRenderer.sortingOrder = -1;
        }
        else
        {
            boxRenderer.sortingOrder = 1;
        }
    }
}
