using TMPro;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    [SerializeField] private AudioClip _potionPickupClip;
    [SerializeField] private AudioClip _powerUpClip;
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
        GameManager.PlaySound(_potionPickupClip);
        potionCount++;
        countText.text = $"x{potionCount}";
    }
    public void SubtractPotion()
    {
        GameManager.PlaySound(_powerUpClip);
        potionCount--;
        if (potionCount < 0) potionCount = 0;
        countText.text = $"x{potionCount}";
    }
}
