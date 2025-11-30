using UnityEngine;

public class Table : MonoBehaviour
{
    private SpriteRenderer boxRenderer;
    private Transform player;

    void Start()
    {
        boxRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Character.OnMoved += Move;
    }

    private void OnDisable()
    {
        Character.OnMoved -= Move;
    }

    // Update is called once per frame
    void Update()
    {
        if(Timer.Instance.Stage == Timer.AgeStage.Kid)
        {
            boxRenderer.sortingOrder = 1;
        }         
    }

    private void Move(Transform playerT)
    {
        if (Timer.Instance.Stage != Timer.AgeStage.Kid)
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
}
