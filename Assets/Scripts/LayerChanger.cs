using UnityEngine;

[RequireComponent(typeof(Character))] // Zabezpieczenie: ten skrypt wymaga skryptu Character obok siebie
public class LayerChanger : MonoBehaviour
{
    [Header("Nazwy Warstw")]
    [SerializeField] private string kidLayerName = "Kid";
    [SerializeField] private string adultLayerName = "Default";
    [SerializeField] private string oldLayerName = "Default";
    [SerializeField] private string deadLayerName = "Default";

    private void OnEnable()
    {
        // 1. SUBSKRYPCJA: Zapisujemy siê na powiadomienia od Character
        Character.OnAgeChanged += HandleAgeChange;
    }

    private void OnDisable()
    {
        // 2. CZYSZCZENIE: Wypisujemy siê, gdy obiekt jest wy³¹czany (wa¿ne!)
        Character.OnAgeChanged -= HandleAgeChange;
    }

    private void Start()
    {
        // 3. INICJALIZACJA: Na starcie gry musimy ustawiæ warstwê rêcznie,
        // bo event OnAgeChanged odpali siê dopiero przy PIERWSZEJ zmianie wieku.
        var character = GetComponent<Character>();
        if (character != null)
        {
            // Rzutujemy Enum na int, bo twoja funkcja UpdateLayer przyjmuje int (zgodnie z eventem)
            HandleAgeChange((int)character.GetAge());
        }
    }

    // Ta funkcja wykona siê automatycznie, gdy Character wywo³a OnAgeChanged?.Invoke()
    private void HandleAgeChange(int ageIndex)
    {
        // Zamieniamy int z powrotem na Enum dla czytelnoœci switcha
        Timer.AgeStage ageState = (Timer.AgeStage)ageIndex;
        string targetLayer = "Default";

        switch (ageState)
        {
            case Timer.AgeStage.Kid:
                targetLayer = kidLayerName;
                break;

            case Timer.AgeStage.Adult:
                targetLayer = adultLayerName;
                break;

            case Timer.AgeStage.Dziad:
                targetLayer = oldLayerName;
                break;

            case Timer.AgeStage.Death:
                targetLayer = deadLayerName;
                break;
        }

        SetLayer(targetLayer);
    }

    private void SetLayer(string layerName)
    {
        int layerId = LayerMask.NameToLayer(layerName);

        if (layerId != -1) // -1 oznacza, ¿e taka warstwa nie istnieje
        {
            gameObject.layer = layerId;
            // Debug.Log($"Zmieniono warstwê na: {layerName}");
        }
        else
        {
            Debug.LogError($"B£¥D: Warstwa '{layerName}' nie istnieje! SprawdŸ Project Settings -> Tags and Layers.");
        }
    }
}