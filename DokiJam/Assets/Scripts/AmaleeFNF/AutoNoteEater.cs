using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class AutoNoteEater : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        var hit = transform.position.y - collision.transform.position.y;
        if (hit <= 5 && hit >= -5)
        {
            Destroy(collision.gameObject);
        }
    }

    void OnTriggerExit2D()
    {
    }

    async Task Update()
    {
    }
}
