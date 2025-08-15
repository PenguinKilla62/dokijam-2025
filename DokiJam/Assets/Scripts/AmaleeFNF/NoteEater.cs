using UnityEngine;

public class NoteEater : MonoBehaviour
{

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioClip audioClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collided with" + collision.gameObject.name);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        var hit = transform.position.y - collision.transform.position.y;
        if (hit <=5 && hit >=-5)
        {
            Destroy(collision.gameObject);
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.PlayOneShot(audioClip);
        }

    }
}
