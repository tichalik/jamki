using UnityEngine;

public class Level : MonoBehaviour
{
    private static Level instance;
    public static Level Instance => instance;

    private void Awake()
    {
        instance = this;
    }
}
