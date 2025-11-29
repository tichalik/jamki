using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2DController : MonoBehaviour
{
    public static Action<Vector2> OnInputChanged;

    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    public IInteractible Item = null;
    [SerializeField] private float interactRadius = 1f;

    private InputSystem_Actions inputActions;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable(); // Enable the "Player" action map
        inputActions.Player.Move.performed += OnMove;  // Subscribe to Move
        inputActions.Player.Move.canceled += OnMove;   // For stopping input

        inputActions.Player.Jump.performed += OnTimeJumpStart;
        inputActions.Player.Jump.canceled += OnTimeJumpEnd;

        inputActions.Player.Interact.performed += OnInteraction;

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

        inputActions.Player.Disable();

        Character.OnAgeChanged -= OnAge;

        Time.timeScale = 1f;
    }

    private void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;
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
        Debug.Log("Interaction clicked");

        if (Item != null)
        {
            Item.Interact(this);
            return;
        }

        int interactionLayer = LayerMask.GetMask("Interaction");

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactRadius, interactionLayer);

        foreach (var h in hits)
        {
            var interactible = h.GetComponent<IInteractible>();

            if (interactible != null && interactible.CanInteract(this))
            {
                Debug.Log("Interacted with: " + h.name);
                interactible.Interact(this);
                return; // stop after FIRST valid one
            }
        }

        Debug.Log("No interaction target.");
    }

    private void OnAge(int age)
    {
        Item?.OnAged(this);
    }
}
