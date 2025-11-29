using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _gamePrefab;

    private static GameManager _instance;
    public static GameManager Instance {  get { return _instance; } }

    private void Awake()
    {
        _instance = this;
    }

    public static void StartGame()
    {
        Instantiate(_instance._gamePrefab, _instance.transform);
    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}
