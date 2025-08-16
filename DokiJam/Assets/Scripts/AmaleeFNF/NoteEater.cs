using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class NoteEater : MonoBehaviour
{

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioClip audioClip;

    [SerializeField]
    InputAction control;

    private bool controlActive = false;

    private bool triggerActive = false;

    private bool miss = false;

    private Game game;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        game = GetComponent<Game>();
    }

    void OnEnable()
    {
        control.Enable(); // Actions must be enabled to receive input
        // control.performed += ControlPerformed;
        // control.canceled += ControlCanceled;
    }

    void OnDisable()
    {
        // control.performed -= ControlPerformed;
        // control.canceled -= ControlCanceled;
        control.Disable();
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

        controlActive = control.IsPressed();
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
