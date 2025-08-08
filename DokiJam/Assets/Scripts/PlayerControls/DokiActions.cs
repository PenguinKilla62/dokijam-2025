using UnityEngine;

public class DokiActions : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] float speed = 5f;
    [SerializeField] int numDragoons = 5;
    [SerializeField] int dragoonNormDmg = 5;
    [SerializeField] int dragoonLongDmg = 5;
    [SerializeField] int dragoonLongRange = 5;
    [SerializeField] int dragoonBeegDmg = 5;
    [SerializeField] int dragoonBeegRange = 5;

    // Objects
    InputSystem_Actions inputActions;
    int currDragoons = 0;

    [Header("References")]
    public Rigidbody2D rb;

    void Awake()
    {
        Debug.Log("DokiActions Awake");
        // Grab all input actions from Unity's New Input System
        inputActions = new InputSystem_Actions();
        // movement
        inputActions.Player.Enable();
        inputActions.Player.Move.Enable();
        inputActions.Player.Move.canceled += _ => Stop();

        // attack
        inputActions.Player.Attack.Enable();

        // interact
        inputActions.Player.Interact.Enable();

        rb = GetComponent<Rigidbody2D>();

        Debug.Log("DokiActions initialized");
    }

    void Stop()
    {
        rb.linearVelocity = Vector2.zero;
        Debug.Log("Movement stopped");
    }

    void normalDragoon()
    {
        // So this is just a normal dragoon
        // CHUCK EM AT ENEMY
        // Left click -> Chuck
        // Long and Beeg can chain react into this, basically chucks it to enemy
        // Damage based on speed? 
        // Call to normalDragoon instantiates a normalDragoon with some velocity in direction of mouse
    }

    void longDragoon()
    {
        // Left click -> swoop cone in front of player
    }

    void beegDragoon()
    {
        // Left click -> bomb, explodes in x seconds
    }

    // Update is called once per frame
    void Update()
    {
        if (inputActions.Player.Move.IsPressed())
        {
            Debug.Log(inputActions.Player.Move.ReadValue<Vector2>());
            rb.linearVelocity = inputActions.Player.Move.ReadValue<Vector2>() * speed;
        }
    }
}
