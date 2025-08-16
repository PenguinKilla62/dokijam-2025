using System.Threading.Tasks;
using UnityEngine;

public class EndBar : MonoBehaviour
{

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioClip audioClip;

    private Game game;

    [SerializeField]
    public bool triggerHit = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject gameObject = GameObject.Find("Game");
        game = gameObject.GetComponent<Game>();
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
        
        Destroy(collision.gameObject);

        if (triggerHit)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.PlayOneShot(audioClip);

            game.MuteUnmuteDoki(true);
            await Task.Delay(100);
            game.MuteUnmuteDoki(false);

            game.ClearHitCombo();
        }
            
            
        

    }
}
