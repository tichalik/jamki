using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Arrow : MonoBehaviour
{
    private RectTransform _rectTransform;
    private Vector3 _controllerPos;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
        _image.enabled = false;
    }

    private void OnEnable()
    {
        Character.OnMoved += RotateTowardsStyrta;
        Localizer.OnUpdatePos += SetPos;
        Character.OnAgeChanged += AgeChange;
    }

    private void OnDisable()
    {
        Character.OnMoved -= RotateTowardsStyrta;
        Localizer.OnUpdatePos -= SetPos;
        Character.OnAgeChanged -= AgeChange;
    }

    private void RotateTowardsStyrta(Transform playerT)
    {
        Vector3 playerPos = playerT.position;

        Vector2 directionXY = new Vector2(
            _controllerPos.x - playerPos.x,
            _controllerPos.y - playerPos.y
        );

        // Compute angle in degrees
        float angle = Mathf.Atan2(directionXY.y, directionXY.x) * Mathf.Rad2Deg;

        // Rotate UI arrow
        _rectTransform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void SetPos(Transform t)
    {
        _controllerPos = t.localPosition;
    }

    private void AgeChange(int age)
    {
        if ((Timer.AgeStage)age == Timer.AgeStage.Dziad)
        {
            _image.enabled = true;
        }
        else
        {
            _image.enabled = false;
        }

    }

    private void Update()
    {
        if(RupiecUI.Instance.hasRupiec)
        {
            _image.enabled = false;
            gameObject.SetActive(false);
        }
    }
}
