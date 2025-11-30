using TMPro;
using UnityEngine;

public class ScoreMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Awake()
    {
        _scoreText.SetText($"{(int)GameManager.Instance._time} sec");
    }
}
