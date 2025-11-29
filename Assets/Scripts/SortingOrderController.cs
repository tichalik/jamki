using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.VFX;

public class SortingOrderController : MonoBehaviour
{
    private Transform player;
    private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var vec2player = transform.position - player.position;
        if (vec2player.y > 0)
        {
            spriteRenderer.sortingOrder = -1;
        }
        else
        {
            spriteRenderer.sortingOrder = 1;
        }
    }
}
