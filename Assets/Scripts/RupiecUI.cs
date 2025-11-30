using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RupiecUI : MonoBehaviour
{
    public static RupiecUI Instance { get; private set; }
    private Image rupiecImage;
    private Image checkmark;
    public bool hasRupiec;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Instance = this;
        if (rupiecImage == null) rupiecImage = GetComponent<Image>();
        Transform childObj = transform.Find("Checkmark");

        if (childObj != null)
        {
            checkmark = childObj.GetComponent<Image>();
            checkmark.enabled = false;
        }
        hasRupiec = false;
    }
    void Start()
    {
        rupiecImage.color = Color.gray;
    }

    public void setRupiecFoundColor()
    {
        rupiecImage.color = new Color(0x6c, 0x8e, 0x7f);
        hasRupiec = true;
        checkmark.enabled = true;
    }
}
