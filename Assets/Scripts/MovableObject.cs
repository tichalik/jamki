using System;
using Unity.VisualScripting;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    [Header("Ustawienia")]
    public float pickUpRange = 1.5f; // Jak blisko trzeba byæ
    public Vector3 holdOffset = new Vector3(0, 0.8f, 0); // Pozycja nad g³ow¹
    public KeyCode interactKey = KeyCode.E;

    [Header("Debug")]
    public bool isCarried = false;

    private Transform player;
    private SpriteRenderer boxRenderer;
    private Character character;
    private int defaultSortingOrder;


    private void OnEnable()
    {
        Character.OnAgeChanged += HandleAgeChange;
    }
    private void OnDisable()
    {
        Character.OnAgeChanged -= HandleAgeChange;
    }
    void Start()
    {
        // ZnajdŸ gracza automatycznie (musi mieæ tag "Player")
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            character = playerObj.GetComponent<Character>();
        }

        boxRenderer = GetComponent<SpriteRenderer>();
        if (boxRenderer)
        {
            defaultSortingOrder = boxRenderer.sortingOrder;
        }
    }

    void Update()
    {
        if (player == null) return;

        // Jeœli naciœniêto E
        if (Input.GetKeyDown(interactKey) && character.GetAge() == Timer.AgeStage.Adult)
        {

            if (isCarried)
            {
                Drop();
            }
            else
            {
                // SprawdŸ dystans do gracza
                float distance = Vector3.Distance(transform.position, player.position);
                if (distance <= pickUpRange)
                {
                    PickUp();
                }
            }
        }
    }

    private void HandleAgeChange(int obj)
    {
        Timer.AgeStage ageState = (Timer.AgeStage)obj;
        if (ageState != Timer.AgeStage.Adult && isCarried)
        {
            Drop();
        }
    }

    private void LateUpdate()
    {
        var vec2player = transform.position - player.position;
        if(vec2player.y > 0)
        {
            boxRenderer.sortingOrder = -1;
        }
        else
        {
            boxRenderer.sortingOrder = 1;
        }
    }

    void PickUp()
    {
        isCarried = true;

        // 1. Wy³¹cz kolizjê (jeœli skrzynka ma collider), ¿eby nie wypycha³a gracza
        // Jeœli nie u¿ywasz colliderów, ten krok jest niepotrzebny, ale bezpieczny
        //var col = GetComponent<Collider2D>();
        //if (col) col.enabled = false;

       var x = (transform.position - player.transform.position);
        x = x.normalized * 0.5f;
        // 2. Przyczep do gracza (Parenting)
        transform.SetParent(player);

        // 3. Ustaw pozycjê nad g³ow¹ (lokalnie wzglêdem gracza)
        transform.localPosition = x;

        // 4. (Opcjonalnie) Zmieñ warstwê sortowania, ¿eby skrzynka by³a NA graczu
        if (boxRenderer) boxRenderer.sortingOrder = 100; // Wysoka wartoœæ
    }

    void Drop()
    {
        isCarried = false;

        // 1. Odczep od gracza (wróæ do œwiata)
        transform.SetParent(null);

        // 2. Zaokr¹glij pozycjê (opcjonalnie), ¿eby skrzynka trafi³a idealnie w kratkê
        // Jeœli chcesz precyzyjnie:
        /*
        transform.position = new Vector3(
            Mathf.Round(transform.position.x),
            Mathf.Round(transform.position.y),
            0
        );
        */

        // 3. W³¹cz kolizjê z powrotem
        var col = GetComponent<Collider2D>();
        if (col) col.enabled = true;

        // 4. Przywróæ warstwê sortowania
        if (boxRenderer) boxRenderer.sortingOrder = defaultSortingOrder;
    }
}