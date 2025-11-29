using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static Action<int> OnAgeChanged;

    [SerializeField]
    private Dictionary<AgeState, List<MonoBehaviour>> _behaviours = new();

    [SerializeField] private AnimationControl _animation;

    public enum AgeState
    {
        Kid,
        Adult,
        Dziad,
        Dead
    }
    [SerializeField]
    private AgeState _state = AgeState.Kid;

    public AgeState GetAge() { return _state; }
    public void OnAged()
    {
        // Disable old state behaviours
        if (_behaviours.ContainsKey(_state))
        {
            foreach (var b in _behaviours[_state])
            {
                b.enabled = false;
            }
        }

        // Move to next state
        if (_state != AgeState.Dead)
        {
            _state = (AgeState)(((int)_state) + 1);
        }

        // Enable new state behaviours
        if (_behaviours.ContainsKey(_state))
        {
            foreach (var b in _behaviours[_state])
            {
                b.enabled = true;
            }
        }

        OnAgeChanged?.Invoke((int)_state);

        switch(_state)
        {
            case AgeState.Kid:
                break;
            case AgeState.Adult:
                break;
            case AgeState.Dziad:
                break;
             case AgeState.Dead:
                break;
        }

        Debug.Log("Character aged to: " + _state);
    }
}
