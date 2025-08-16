using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
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

    private Dictionary<string, bool> activeTriggerList = new Dictionary<string, bool>();

    private bool miss = false;

    private Game game;

    private InputSystem_Actions inputActions;

    [SerializeField]
    public float input_health_timeout_secs = 1.5f;


    private float current_input_health_timeout_secs = 1.5f;

    private bool input_health_timeout = false;


    void Awake()
    {
        
        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();
        inputActions.Player.Arrows.Enable();
        
    }

    public void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.Arrows.Disable();
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
        activeTriggerList.Add(collision.gameObject.GetHashCode().ToString(), true);
    } 

    void OnTriggerStay2D(Collider2D collision)
    {
        var hit = transform.position.y - collision.transform.position.y;
        if (controlActive && hit <= 20 && hit >= -20)
        {
            activeTriggerList[collision.gameObject.GetHashCode().ToString()] = false;
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

    void OnTriggerExit2D(Collider2D collider)
    {
        game.AddHealth(-1);
        triggerActive = false;
        var hashCode = collider.gameObject.GetHashCode().ToString();

        if (activeTriggerList.ContainsKey(hashCode))
        {
            activeTriggerList[hashCode] = false;
        }
    }

    void ChangeDokiAnimation(characterAnimator.AnimationState animationState)
    { 
        var gameObject = GameObject.Find("doki");
        var characterAnimator = gameObject.GetComponent<characterAnimator>();
        characterAnimator.currentAnimation = animationState;
        characterAnimator.IdleTimeout = 2;
    }

    async Task Update()
    {
        if (inputActions.Player.Arrows.IsPressed())
        {
            Debug.Log(inputActions.Player.Arrows.ReadValue<Vector2>());
            var readAxis = inputActions.Player.Arrows.ReadValue<Vector2>();
            controlActive = false;
            if (readAxis == Vector2.left && noteType == NoteType.left)
            {
                controlActive = true;

                ChangeDokiAnimation(characterAnimator.AnimationState.Left);
            }
            else if (readAxis == Vector2.down && noteType == NoteType.down)
            {
                controlActive = true;

                ChangeDokiAnimation(characterAnimator.AnimationState.Down);
            }
            else if (readAxis == Vector2.up && noteType == NoteType.up)
            {
                controlActive = true;

                ChangeDokiAnimation(characterAnimator.AnimationState.Up);
            }
            else if (readAxis == Vector2.right && noteType == NoteType.right)
            {
                controlActive = true;

                ChangeDokiAnimation(characterAnimator.AnimationState.Right);
            }
        }


        if (controlActive)
        {
            if (!activeTriggerList.ContainsValue(true))
            {
                if (!input_health_timeout)
                {
                    game.AddHealth(-1);
                    miss = true;
                    controlActive = false;
                    input_health_timeout = true;
                    current_input_health_timeout_secs = input_health_timeout_secs;
                }
            }
        }

        if (miss)
        {
            await Task.Delay(100);
            miss = false;
        }

        if (input_health_timeout == true && current_input_health_timeout_secs > 0)
        {
            current_input_health_timeout_secs -= Time.deltaTime;
        }
        else if (input_health_timeout && current_input_health_timeout_secs <= 0)
        {
            input_health_timeout = false;
        }
    }
}
