using UnityEngine;

public class Table : MonoBehaviour
{
    private SpriteRenderer boxRenderer;
    private Transform player;

    void Start()
    {
        boxRenderer = GetComponent<SpriteRenderer>();
    }
 
    // Update is called once per frame
    void Update()
    {
        if(Timer.Instance.Stage == Timer.AgeStage.Kid)
        {
            boxRenderer.sortingOrder = 1;
        } 
        else
        {
            var vec2player = transform.position - player.position;
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
