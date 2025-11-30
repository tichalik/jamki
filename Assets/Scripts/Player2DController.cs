using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2DController : MonoBehaviour
{
    public static Action<Vector2> OnInputChanged;

    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    public Interactible Item = null;
    [SerializeField] private float interactRadius = 1f;

    private InputSystem_Actions inputActions;
    private Interactible _shownItem = null;
    private int interactionLayerMask;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new InputSystem_Actions();
        interactionLayerMask = LayerMask.GetMask("Interaction");
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();

        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;

        inputActions.Player.Jump.performed += OnTimeJumpStart;
        inputActions.Player.Jump.canceled += OnTimeJumpEnd;

        inputActions.Player.Interact.performed += OnInteraction;

        inputActions.Player.Crouch.performed += OnPotionUsage;

        Character.OnAgeChanged += OnAge;

        Time.timeScale = 1f;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;

        inputActions.Player.Jump.performed -= OnTimeJumpStart;
        inputActions.Player.Jump.canceled -= OnTimeJumpEnd;

        inputActions.Player.Interact.performed -= OnInteraction;
        inputActions.Player.Crouch.performed -= OnPotionUsage;

        Character.OnAgeChanged -= OnAge;

        inputActions.Player.Disable();

        Time.timeScale = 1f;
    }

    private void Update()
    {
        // Movement (Unity 6 uses linearVelocity)
        rb.linearVelocity = moveInput * moveSpeed;

        // If carrying an item, just highlight that
        if (Item != null)
        {
            if (_shownItem != Item)
            {
                _shownItem?.Hide();
                _shownItem = Item;
                _shownItem.Show();
            }
            return;
        }

        // Detect interactibles
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactRadius, interactionLayerMask);

        Interactible nearest = null;
        float nearestDist = float.MaxValue;

        foreach (var h in hits)
        {
            var interactible = h.GetComponent<Interactible>();
            if (interactible != null && interactible.CanInteract(this))
            {
                float d = Vector2.Distance(transform.position, h.transform.position);
                if (d < nearestDist)
                {
                    nearestDist = d;
                    nearest = interactible;
                }
            }
        }

        // If no interactible found -> hide previous
        if (nearest == null)
        {
            if (_shownItem != null)
            {
                _shownItem.Hide();
                _shownItem = null;
            }
            return;
        }

        // If new nearest interactible is different → switch highlight
        if (_shownItem != nearest)
        {
            _shownItem?.Hide();
            _shownItem = nearest;
            _shownItem.Show();
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        OnInputChanged?.Invoke(moveInput);
    }

    private void OnTimeJumpStart(InputAction.CallbackContext context)
    {
        Time.timeScale = 10f;
    }

    private void OnTimeJumpEnd(InputAction.CallbackContext context)
    {
        Time.timeScale = 1f;
    }

    private void OnInteraction(InputAction.CallbackContext context)
    {
        if (_shownItem != null)
        {
            _shownItem.Interact(this);
        }
    }

    private void OnPotionUsage(InputAction.CallbackContext context)
    {
        if (PotionManager.Instance.CanUsePotion())
        {
            Timer.Instance.RevertTime();
            PotionManager.Instance.SubtractPotion();
        }
    }

    private void OnAge(int age)
    {
        Item?.OnAged(this);

        if((Timer.AgeStage)age == Timer.AgeStage.Death)
        {
            enabled = false;
        }
    }
}
