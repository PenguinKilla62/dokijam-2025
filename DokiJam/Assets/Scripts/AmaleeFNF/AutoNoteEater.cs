using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class AutoNoteEater : MonoBehaviour
{

    [SerializeField]
    characterAnimator.AnimationState arrow = characterAnimator.AnimationState.Left;

    [SerializeField]
    public Image image;

    [SerializeField]
    public Color activeHitColor;

    private Color originalColor;

    [SerializeField]
    public float activeColorShowSecs = 0.1f;

    private float currentColorShowSecs = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalColor = image.color;
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

            currentColorShowSecs = activeColorShowSecs;
            image.color = activeHitColor;
        }
    }

    void OnTriggerExit2D()
    {
    }

    async Task Update()
    {
        if (currentColorShowSecs <= 0)
        {
            image.color = originalColor;
        }
        else
        {
            currentColorShowSecs -= Time.deltaTime;
        }
    }
}
