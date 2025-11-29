using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void OnEnable()
    {
        Player2DController.OnInputChanged += OnInputChanged;
        Character.OnAgeChanged += OnAgeChanged;
    }

    private void OnDisable()
    {
        Player2DController.OnInputChanged -= OnInputChanged;
        Character.OnAgeChanged -= OnAgeChanged;
    }

    private void OnInputChanged(Vector2 input)
    {
        if(input.x > 0)
        {
            var scale = _animator.transform.localScale;
            scale.x = -Mathf.Abs(scale.x);

            _animator.transform.localScale = scale;
        }
        else
        {
            var scale = _animator.transform.localScale;
            scale.x = Mathf.Abs(scale.x);

            _animator.transform.localScale = scale;
        }

        _animator.SetFloat("SpeedX", Mathf.Abs(input.x));
        _animator.SetFloat("SpeedY", input.y);
    }

    private void OnAgeChanged(int value)
    {
        _animator.SetInteger("Age", value);
    }
}
