using UnityEngine;

public class normalDragoonHit : MonoBehaviour
{
    public int damage = 5; // Damage dealt by the normal dragoon
    public float speed = 5f; // Speed of the normal dragoon
                             // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name + " hit by normal dragoon");
        Vector2 direction = (transform.position - collision.transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            -direction,
            100f);
        Vector2 normal = hit.normal;
        this.GetComponent<Rigidbody2D>().linearVelocity += normal * speed;
        Debug.DrawRay(hit.point, normal, Color.red, 1f);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
