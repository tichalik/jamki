using System;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static Action<Transform> OnMoved;
    public static Action<int> OnAgeChanged;

    [SerializeField]
    private Dictionary<Timer.AgeStage, List<MonoBehaviour>> _behaviours = new();
    [SerializeField] private AnimationControl _animation;

    public Timer.AgeStage GetAge() { return Timer.Instance.Stage; }

    private void OnEnable()
    {
        Timer.OnAgeChanged += OnAged;
    }

    private void OnDisable()
    {
        Timer.OnAgeChanged -= OnAged;
    }
    private void OnAged(Timer.AgeStage oldStage, Timer.AgeStage newStage)
    {
        // Disable old state behaviours
        if (_behaviours.ContainsKey(oldStage))
        {
            foreach (var b in _behaviours[oldStage])
            {
                b.enabled = false;
            }
        }

        // Enable new state behaviours
        if (_behaviours.ContainsKey(newStage))
        {
            foreach (var b in _behaviours[newStage])
            {
                b.enabled = true;
            }
        }

        OnAgeChanged?.Invoke((int)newStage);

        switch(newStage)
        {
            case Timer.AgeStage.Kid:
                break;
            case Timer.AgeStage.Adult:
                break;
            case Timer.AgeStage.Dziad:
                break;
             case Timer.AgeStage.Death:
                break;
        }
    }

    private void LateUpdate()
    {
        OnMoved?.Invoke(transform);
    }
}
