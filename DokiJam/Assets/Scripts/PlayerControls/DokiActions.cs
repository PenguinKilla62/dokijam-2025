using UnityEngine;
using System.Collections;

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

    // Objects and Other Variables
    InputSystem_Actions inputActions;
    Animator animator;
    int currDragoons = 0;
    int currWeapon = 0; // 0 = normal, 1 = long, 2 = beeg
    float elapsedTime = 0f;
    bool isSweeping = false;
    int currAttackLong = 0;
    int[] longDragoonAttacks = { 45, 180, 360 }; // Attack types for long dragoon
    private PolygonCollider2D poly;
    bool hasBeeg = false;

    [Header("References")]
    public Rigidbody2D rb;
    public GameObject normalDragoonPrefab;
    public GameObject longDragoonPrefab;
    public GameObject beegDragoonPrefab;

    void Awake()
    {
        poly = GetComponent<PolygonCollider2D>();
        poly.isTrigger = true;
        poly.enabled = false; // Clear the polygon collider points
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
            switch (currWeapon)
            {
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
        tossedDragoon.GetComponent<Rigidbody2D>().linearDamping = 0.25f;
        currDragoons++;
    }

    void longDragoon()
    {
        // Left click -> swoop cone in front of player
        Debug.Log("Long dragoon attack executed");
        // var hittingDragoon = Instantiate(longDragoonPrefab, transform.position + Vector3.right, Quaternion.Euler(0, 0, 45));
        // Vector2 mousePosition = Camera.main.ScreenToWorldPoint(inputActions.Player.MousePos.ReadValue<Vector2>());
        // hittingDragoon.GetComponent<Transform>().RotateAround(this.transform.position, mousePosition, 45f);
        isSweeping = true; // Set sweeping to true to allow for sweeping attacks
    }

    void beegDragoon()
    {
        // Left click -> bomb, explodes in x seconds
        Debug.Log("Beeg dragoon attack executed");
        if (!hasBeeg)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(inputActions.Player.MousePos.ReadValue<Vector2>());
            var direction = (mousePosition - transform.position).normalized;
            var beegDragoon = Instantiate(beegDragoonPrefab, transform.position + direction * 2, Quaternion.identity);
            StartCoroutine(beegDragoonExplosion(beegDragoon));
        }
    }

    IEnumerator beegDragoonExplosion(GameObject beegDragoon)
    {
        hasBeeg = true;
        // Wait for a few seconds before exploding
        yield return new WaitForSeconds(3f);
        var polyBeeg = beegDragoon.GetComponent<PolygonCollider2D>();
        polyBeeg.isTrigger = true;
        // Create polygon, 360 degrees around the beeg dragoon
        int pointCount = 180; // Number of points in the polygon
        Vector2[] points = new Vector2[pointCount];
        points[0] = beegDragoon.transform.position; // Center point
        for (int i = 0; i < pointCount - 1; i++)
        {
            float angle = -180 + 360 / (pointCount - 1) * i;
            float rad = angle * Mathf.Deg2Rad;
            points[i + 1] = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * dragoonBeegRange;
            // Debug.Log("Point " + i + ": " + points[i + 1]);
        }
        polyBeeg.points = points;
        yield return new WaitForSeconds(1.0f); // Small delay
        Debug.Log("Beeg dragoon explosion!");
        // Instantiate explosion effect here
        // Deal damage to enemies in range
        Destroy(beegDragoon);
        hasBeeg = false;
    }

    void sweepLong(int attackIndex)
    {
        if (!isSweeping) return;
        poly.enabled = true;
        // Debug.Log("Sweeping with long dragoon, attack index: " + attackIndex);
        // First attack sweeps in a 45 degree arc in direction of mouse
        // Second attack sweeps in a 90 degree arc in direction of mouse, moves doki slightly in direction of mouse
        // Third attack sweeps in a 360 degree arc 
        float t = Mathf.Clamp01(elapsedTime / timeBetweenAttacks);
        // Debug.Log("Sweep time: " + t);
        if (t >= 1f)
        {
            isSweeping = false; // Reset sweeping after the attack is done
            currAttackLong = (currAttackLong + 1) % 3; // Cycle through attacks
            poly.points = new Vector2[] {  }; // Clear the polygon collider points
            poly.enabled = false;
            return;
        }
        float angleWanted = Mathf.Lerp(0, longDragoonAttacks[currAttackLong], t);

        // Debug.Log("Sweeping long dragoon with angle: " + angleWanted);
        // set points of the polygon collider to the angle
        int pointCnt = Mathf.CeilToInt(angleWanted/2f) + 1;
        // Debug.Log("Number of points in polygon: " + pointCnt);
        Vector2[] points = new Vector2[pointCnt];
        points[0] = (Camera.main.ScreenToWorldPoint(inputActions.Player.MousePos.ReadValue<Vector2>()) - transform.position).normalized * dragoonLongRange; // center
        float halfArc = angleWanted / 2f;
        Vector2 dirToMouse = (Camera.main.ScreenToWorldPoint(inputActions.Player.MousePos.ReadValue<Vector2>()) - transform.position).normalized;
        float mouseAngle = Mathf.Atan2(dirToMouse.y, dirToMouse.x) * Mathf.Rad2Deg;
        for (int i = 0; i < pointCnt-1; i++)
        {
            float angle = mouseAngle - halfArc + angleWanted / (pointCnt - 1) * i;
            float rad = angle * Mathf.Deg2Rad;
            points[i + 1] = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * dragoonLongRange;
            // Debug.Log("Point " + i + ": " + points[i + 1]);
        }
        poly.points = points;
        // Debug.Log(poly.points.Length + " points set for long dragoon sweep");


        // var sweepingDragoon = Instantiate(longDragoonPrefab, transform.position, Quaternion.identity);
        // sweepingDragoon.GetComponent<longDragoonHit>().attacks = new int[] { attackIndex };
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        MoveAndAttack();
        sweepLong(currAttackLong);
    }
}
