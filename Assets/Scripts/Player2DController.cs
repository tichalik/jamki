using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2DController : MonoBehaviour
{
    public static Action<Vector2> OnInputChanged;

    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

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
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Disable();
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
}
