using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static event Action<AgeStage, AgeStage> OnAgeChanged;

    private static Timer _instance;
    public static Timer Instance => _instance;

    public enum AgeStage
    {
        Kid,
        Adult,
        Dziad,
        Death
    }

    private AgeStage _stage = AgeStage.Kid;
    public AgeStage Stage => _stage;

    [SerializeField] private Image _timeImage;
    private Color _startColor = Color.yellow;
    private Color _endColor = Color.red;

    private float _time = 0f;
    private float _maxTime = 120f;

    private void Awake()
    {
        _instance = this;
        _timeImage.fillAmount = 0;
        _timeImage.color = Color.Lerp(_startColor, _endColor, 0);
    }

    void Update()
    {
        _time += Time.deltaTime;

        float fill = _time / _maxTime;

        _timeImage.fillAmount = fill;
        _timeImage.color = Color.Lerp(_startColor, _endColor, fill);

        if (_time >= _maxTime)
        {
            if (_stage != AgeStage.Death)
                SetStage(AgeStage.Death);

            enabled = false;
            return;
        }

        if (_time >= 2f * _maxTime / 3f)
            SetStage(AgeStage.Dziad);
        else if (_time >= _maxTime / 3f)
            SetStage(AgeStage.Adult);
        else
            SetStage(AgeStage.Kid);
    }

    private void SetStage(AgeStage newStage)
    {
        if (_stage == newStage)
            return;

        var oldStage = _stage;
        _stage = newStage;
        Debug.Log("Stage = " + newStage);

        OnAgeChanged?.Invoke(oldStage, newStage);
    }

    public void RevertTime()
    {
        _time -= _maxTime * 0.5f;
        if (_time < 0f)
            _time = 0f;
    }
}
