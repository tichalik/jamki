using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    public static Action OnRestart;

    [SerializeField] private GameObject _gamePrefab;
    [SerializeField] private GameObject _mainMenuPrefab;
    [SerializeField] private GameObject _gameUIPrefab;
    [SerializeField] private GameObject _scoreUIPrefab;
    [SerializeField] private GameObject _deadMenuPrefab;
    [SerializeField] private Transform _canvas;

    private AudioSource _audioSource;
    private static GameManager _instance;
    public static GameManager Instance {  get { return _instance; } }

    public float _time = 0;

    private void Awake()
    {
        _instance = this;
        Instantiate(_mainMenuPrefab, _canvas);

        _audioSource = GetComponent<AudioSource>();
    }

    public static void StartGame()
    {
        _instance._time = Time.time;
        Instantiate(_instance._gamePrefab, _instance.transform);
        Instantiate(_instance._gameUIPrefab, _instance._canvas);
    }

    public static void RestartGame()
    {
        OnRestart?.Invoke();
        Instantiate(_instance._gamePrefab, _instance.transform);
        Instantiate(_instance._gameUIPrefab, _instance._canvas);
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    public static void EndGame()
    {
        OnRestart?.Invoke();
        _instance._time = Time.time - _instance._time;
        Instantiate(_instance._scoreUIPrefab, _instance._canvas);
    }

    public static void GameOver()
    {
        OnRestart?.Invoke();
        Instantiate(_instance._deadMenuPrefab, _instance._canvas);
    }

    public static void PlaySound(AudioClip clip)
    {
        _instance._audioSource.PlayOneShot(clip);
    }
}
