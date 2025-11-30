using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenScroll : MonoBehaviour
{
    [SerializeField] private List<Image> _images;
    [SerializeField] private float scrollSpeed = 50f;

    private float spacing; // auto-calculated from first image

    private void Start()
    {
        if (_images.Count == 0)
            return;

        // Use the height of the first image as spacing
        RectTransform rt = _images[0].rectTransform;
        spacing = rt.rect.height;

        _images[0].rectTransform.anchoredPosition = new Vector2(0, Screen.height);

        // Position them vertically using the calculated spacing
        for (int i = 1; i < _images.Count; i++)
        {
            _images[i].rectTransform.anchoredPosition =
                rt.anchoredPosition - new Vector2(0, spacing * i);
        }
    }

    private void Update()
    {
        if (_images.Count == 0)
            return;

        // Move only the first image
        RectTransform first = _images[0].rectTransform;
        first.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        // Position all others relative to first one
        for (int i = 1; i < _images.Count; i++)
        {
            _images[i].rectTransform.anchoredPosition =
                first.anchoredPosition - new Vector2(0, spacing * i);
        }

        // When first goes above screen, recycle it
        if (first.anchoredPosition.y > Screen.height + spacing)
        {
            // Move it to bottom
            first.anchoredPosition -= new Vector2(0, spacing * _images.Count);

            // Rotate list so the next image becomes the new "first"
            Image moved = _images[0];
            _images.RemoveAt(0);
            _images.Add(moved);
        }
    }
}
