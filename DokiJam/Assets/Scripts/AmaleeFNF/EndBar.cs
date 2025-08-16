using System.Threading.Tasks;
using UnityEngine;

public class EndBar : MonoBehaviour
{

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioClip audioClip;

    private Game game;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        game = GetComponent<Game>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collided with" + collision.gameObject.name);
    }

    async Task OnTriggerStay2D(Collider2D collision)
    {
        var hit = transform.position.y - collision.transform.position.y;
        if (hit <= 5 && hit >= -5)
        {
            Destroy(collision.gameObject);
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.PlayOneShot(audioClip);

            game.MuteUnmuteDoki(true);
            await Task.Delay(50);
            game.MuteUnmuteDoki(false);

            game.ClearHitCombo();
            
            
        }

    }
}
