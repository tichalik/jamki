using UnityEngine;

public class DestroyOnRestart : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.OnRestart += Restart;
    }

    private void OnDisable()
    {
        GameManager.OnRestart -= Restart;
    }

    private void Restart()
    {
        Destroy(gameObject);
    }
}
