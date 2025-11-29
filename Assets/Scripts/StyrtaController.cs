using UnityEngine;

public class StyrtaController : MonoBehaviour
{
    [Header("Wygl¹d")]
    [SerializeField] private Sprite trashSprite;
    [SerializeField] private Sprite hiddenItemSprite;

    [Header("Ustawienia")]
    [SerializeField] private string dialogueMessage = "Znalaz³eœ stary telefon!";
    [SerializeField] private string item = "telefon";

    private SpriteRenderer spriteRenderer;
    private bool isRevealed = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        // Zapisujemy siê na zmianê wieku
        Character.OnAgeChanged += HandleAgeChange;
    }

    private void OnDisable()
    {
        Character.OnAgeChanged -= HandleAgeChange;
    }

    private void Start()
    {
        HandleAgeChange(1);
    }

    // Ta funkcja zmienia wygl¹d AUTOMATYCZNIE
    private void HandleAgeChange(int ageIndex)
    {
        Character.AgeState state = (Character.AgeState)ageIndex;

        if (state == Character.AgeState.Dziad)
        {
            isRevealed = true;
            spriteRenderer.sprite = hiddenItemSprite;
        }
        else
        {
            isRevealed = false;
            spriteRenderer.sprite = trashSprite;
        }
    }

    // Ta funkcja jest wywo³ywana przez gracza klawiszem (np. E)
    public void Interact()
    {
        if (isRevealed)
        {
            // LOGIKA DLA DZIADA (Sukces)
            Debug.Log($"<color=green>SUKCES:</color> {dialogueMessage}");

            // Tu mo¿esz dodaæ przedmiot do ekwipunku, np:
            // Inventory.AddItem("Zegarek");

            // Opcjonalnie: Zniszcz stertê po zebraniu
            // Destroy(gameObject);
        }
        else
        {
            // LOGIKA DLA RESZTY (Pora¿ka)
            Debug.Log("To tylko kupa œmierdz¹cych œmieci. Nie bêdê w tym grzebaæ.");
        }
    }
}
