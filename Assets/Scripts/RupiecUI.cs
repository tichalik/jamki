using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RupiecUI : MonoBehaviour
{
    public static RupiecUI Instance { get; private set; }
    private Image rupiecImage;
    public bool hasRupiec;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        if (rupiecImage == null) rupiecImage = GetComponent<Image>();
        hasRupiec = false;
    }
    void Start()
    {
        rupiecImage.color = Color.gray;
    }

    public void setRupiecFoundColor()
    {
        rupiecImage.color = Color.green;
        hasRupiec = true;
    }
}
