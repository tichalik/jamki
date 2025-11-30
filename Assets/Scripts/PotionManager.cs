using TMPro;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    public static PotionManager Instance { get; private set; }

    private TextMeshProUGUI countText;
    private int potionCount = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int GetPotionCount() {  return potionCount; }
    public bool CanUsePotion()
    {
        return potionCount > 0;
    }
    private void Awake()
    {
            Instance = this;
        if (countText == null) countText = GetComponent<TextMeshProUGUI>();
        potionCount = 0;
    }
    void Start()
    {
        countText.text = $"x{potionCount}";
    }

    public void AddPotion()
    {
        potionCount++;
        countText.text = $"x{potionCount}";
    }
    public void SubtractPotion()
    {
        potionCount--;
        if (potionCount < 0) potionCount = 0;
        countText.text = $"x{potionCount}";
    }
}
