using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class NoteEater : MonoBehaviour
{

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioClip audioClip;

    [SerializeField]
    public enum NoteType
    {
        left = 0,
        down = 1,
        up = 2,
        right = 3
    };

    [SerializeField]
    public NoteType noteType;

    private bool controlActive = false;

    private bool triggerActive = false;

    private bool miss = false;

    private Game game;

    private InputSystem_Actions inputActions;


    void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var gameObject = GameObject.Find("Game");
        game = gameObject.GetComponent<Game>();
        Debug.Log(game);
    }

    void ControlPerformed(InputAction.CallbackContext context)
    {
        controlActive = true;
    }

    void ControlCanceled(InputAction.CallbackContext context)
    {
        controlActive = false;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collided with" + collision.gameObject.name);
        triggerActive = true;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        var hit = transform.position.y - collision.transform.position.y;
        if (controlActive && hit <= 5 && hit >= -5)
        {
            Destroy(collision.gameObject);
            triggerActive = false;
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.PlayOneShot(audioClip);
            game.AddHealth(1);
            game.AddHitCombo();
        }
    }

    void OnTriggerExit2D()
    {
        game.AddHealth(-1);
        triggerActive = false;
    }

    async Task Update()
    {

        var readAxis = inputActions.Player.Arrows.ReadValue<Vector2>();
        controlActive = false;
        if (readAxis == Vector2.left && noteType == NoteType.left)
        {
            controlActive = true;
        }
        else if (readAxis == Vector2.down && noteType == NoteType.down)
        {
            controlActive = true;
        }
        else if (readAxis == Vector2.up && noteType == NoteType.up)
        {
            controlActive = true;
        }
        else if (readAxis == Vector2.right && noteType == NoteType.right)
        {
            controlActive = true;
        }

        
        if (controlActive)
            {

                if (triggerActive == false)
                {
                    game.AddHealth(-1);
                    miss = true;
                }
            }

        if (miss)
        {
            await Task.Delay(100);
            miss = false;
        }
    }
}
