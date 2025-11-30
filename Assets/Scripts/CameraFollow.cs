using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private void OnEnable()
    {
        Character.OnMoved += MoveCam;
    }

    private void OnDisable()
    {
        Character.OnMoved -= MoveCam;
    }

    private void MoveCam(Transform t)
    {
        var pos = t.position;
        pos.z = transform.position.z;

        transform.position = pos;
    }
}
