using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(PlayButton);
        _quitButton.onClick.AddListener(QuitButton);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(PlayButton);
        _quitButton.onClick.RemoveListener(QuitButton);
    }

    private void PlayButton()
    {
        GameManager.StartGame();
        Destroy(gameObject);
    }

    private void QuitButton()
    {
        GameManager.QuitGame();
    }
}
