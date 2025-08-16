using UnityEngine;

public class BumpMint : MonoBehaviour
{
    public bool bumpMintOnTrigger = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // has to be the player cuz nothing else moves lmao
        if (bumpMintOnTrigger)
        {
            YarnCommandHandler yarnCommandHandler = FindFirstObjectByType<YarnCommandHandler>();
            yarnCommandHandler.PlayYarn("MintStage_Start");
            bumpMintOnTrigger = false;
        }
    }
}
