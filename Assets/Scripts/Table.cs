using UnityEngine;

public class Table : MonoBehaviour
{
    private SpriteRenderer boxRenderer;
    private Transform player;
    private Character character;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            character = player.GetComponent<Character>();
        }
        boxRenderer = GetComponent<SpriteRenderer>();
    }
 
    // Update is called once per frame
    void Update()
    {
        if(character.GetAge() == Character.AgeState.Kid)
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
