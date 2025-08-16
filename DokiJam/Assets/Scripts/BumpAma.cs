using UnityEngine;

public class BumpAma : MonoBehaviour
{
    public bool bumpAmaOnTrigger = true;
    public Sprite[] sprites;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Update the sprite based on time
        GetComponent<SpriteRenderer>().sprite = sprites[(int)(Time.time * 5) % sprites.Length];
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // has to be the player cuz nothing else moves lmao
        if (bumpAmaOnTrigger)
        {
            YarnCommandHandler yarnCommandHandler = FindFirstObjectByType<YarnCommandHandler>();
            yarnCommandHandler.PlayYarn("AmaStage_Start");
            bumpAmaOnTrigger = false;
        }
    }
}
