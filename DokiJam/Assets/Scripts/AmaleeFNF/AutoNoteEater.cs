using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class AutoNoteEater : MonoBehaviour
{

    [SerializeField]
    characterAnimator.AnimationState arrow = characterAnimator.AnimationState.Left;

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

            var gameObject = GameObject.Find("ama");
            var characterAnimator = gameObject.GetComponent<characterAnimator>();
            characterAnimator.currentAnimation = arrow;
            characterAnimator.IdleTimeout = 2;
        }
    }

    void OnTriggerExit2D()
    {
    }

    async Task Update()
    {
    }
}
