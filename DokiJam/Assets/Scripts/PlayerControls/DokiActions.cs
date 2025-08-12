using UnityEngine;

public class DokiActions : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] float speed = 5f;
    [SerializeField] float sprintSpeed = 10f;
    [SerializeField] int numDragoons = 5;
    [SerializeField] int dragoonNormDmg = 5;
    [SerializeField] int dragoonLongDmg = 5;
    [SerializeField] int dragoonLongRange = 5;
    [SerializeField] int dragoonBeegDmg = 5;
    [SerializeField] int dragoonBeegRange = 5;
    [SerializeField] float timeBetweenAttacks = 0.5f;

    // Objects
    InputSystem_Actions inputActions;
    Animator animator;
    int currDragoons = 0;
    int currWeapon = 0; // 0 = normal, 1 = long, 2 = beeg
    float elapsedTime = 0f;

    [Header("References")]
    public Rigidbody2D rb;
    public GameObject normalDragoonPrefab;
    public GameObject longDragoonPrefab;
    public GameObject beegDragoonPrefab;

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

        // look
        inputActions.Player.MousePos.Enable();

        // interact
        inputActions.Player.Interact.Enable();

        // weapon swap
        inputActions.Player.WeaponSwapMouse.Enable();

        // sprint
        inputActions.Player.Sprint.Enable();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        Debug.Log("DokiActions initialized");
    }

    void Stop()
    {
        rb.linearVelocity = Vector2.zero;
        Debug.Log("Movement stopped");
        animator.SetFloat("xVelocity", 0f);
    }

    void MoveAndAttack()
    {
        if (inputActions.Player.Move.IsPressed())
        {
            Debug.Log(inputActions.Player.Move.ReadValue<Vector2>());
            if (inputActions.Player.Move.ReadValue<Vector2>() == Vector2.up)
            {
                // up
                animator.SetFloat("vVelocity", 1f);
                animator.SetFloat("hVelocity", 0f);
                animator.SetBool("isBackward", true);
            }
            else if (inputActions.Player.Move.ReadValue<Vector2>() == Vector2.down)
            {
                // down
                animator.SetFloat("vVelocity", -1f);
                animator.SetFloat("hVelocity", 0f);
                animator.SetBool("isBackward", false);
            }
            else if (inputActions.Player.Move.ReadValue<Vector2>() == Vector2.left)
            {
                // left
                animator.SetFloat("hVelocity", -1f);
                animator.SetFloat("vVelocity", 0f);
            }
            else if (inputActions.Player.Move.ReadValue<Vector2>() == Vector2.right)
            {
                // right
                animator.SetFloat("hVelocity", 1f);
                animator.SetFloat("vVelocity", 0f);
            }
            rb.linearVelocity = inputActions.Player.Move.ReadValue<Vector2>() * speed;
            animator.SetFloat("xVelocity", 0.4f);
            // handle sprinting
            if (inputActions.Player.Sprint.IsPressed())
            {
                rb.linearVelocity *= sprintSpeed / speed;
                animator.SetFloat("xVelocity", 1f);
            }
        }

        if (inputActions.Player.WeaponSwapMouse.IsPressed())
        {
            Debug.Log(inputActions.Player.WeaponSwapMouse.ReadValue<Vector2>());
            currWeapon += (int)inputActions.Player.WeaponSwapMouse.ReadValue<Vector2>()[1];
            if (currWeapon < 0) currWeapon += 3;
            if (currWeapon > 2) currWeapon -= 3;
            Debug.Log("Current weapon: " + currWeapon);
        }

        if (inputActions.Player.Attack.IsPressed() && elapsedTime >= timeBetweenAttacks)
        {
            elapsedTime = 0f; // Reset the attack timer
            Debug.Log("Attack pressed");
            switch (currWeapon) {
                case 0:
                    normalDragoon();
                    break;
                case 1:
                    longDragoon();
                    break;
                case 2:
                    beegDragoon();
                    break;
                default:
                    Debug.LogError("Invalid weapon type selected: " + currWeapon);
                    break;
            }
        }
    }

    void normalDragoon()
    {
        // So this is just a normal dragoon
        // CHUCK EM AT ENEMY
        // Left click -> Chuck
        // Long and Beeg can chain react into this, basically chucks it to enemy
        // Damage based on speed? 
        // Call to normalDragoon instantiates a normalDragoon with some velocity in direction of mouse
        Debug.Log("Normal dragoon attack executed");
        if (currDragoons >= numDragoons)
        {
            Debug.Log("Max number of dragoons reached");
            return; // Don't spawn more dragoons than allowed
        }
        var tossedDragoon = Instantiate(normalDragoonPrefab, transform.position, Quaternion.identity);
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(inputActions.Player.MousePos.ReadValue<Vector2>());
        Vector2 direction = (mousePosition - rb.position).normalized;
        tossedDragoon.GetComponent<Rigidbody2D>().linearVelocity = (direction * speed) + rb.linearVelocity;
        currDragoons++;
    }

    void longDragoon()
    {
        // Left click -> swoop cone in front of player
        Debug.Log("Long dragoon attack executed");
        var tossedDragoon = Instantiate(longDragoonPrefab, transform.position, Quaternion.identity);
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void beegDragoon()
    {
        // Left click -> bomb, explodes in x seconds
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        MoveAndAttack();
    }
}
