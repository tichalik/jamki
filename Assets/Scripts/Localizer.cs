using System;
using UnityEngine;

public class Localizer : MonoBehaviour
{
    public static Action<Transform> OnUpdatePos;

    private void Start()
    {
        OnUpdatePos?.Invoke(transform);
    }
}
