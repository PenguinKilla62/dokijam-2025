using UnityEngine;

public class office_events : MonoBehaviour
{
    public BoxCollider2D bossDoorCollider;
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
        YarnCommandHandler yarnCommandHandler = FindFirstObjectByType<YarnCommandHandler>();
        yarnCommandHandler.SeeBoss();
    }
}
