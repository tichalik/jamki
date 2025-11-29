using UnityEngine;

public class Potion : MonoBehaviour
{
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PotionManager.Instance.AddPotion();
            Destroy(gameObject);
        }
    }
}
