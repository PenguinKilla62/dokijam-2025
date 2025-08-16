using UnityEngine;
using UnityEngine.SceneManagement;

public class MintIsekai : MonoBehaviour
{
    public BoxCollider2D mintDoorCollider;
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
        FadeToBlack fadeToBlack = FindFirstObjectByType<FadeToBlack>();
        fadeToBlack.FadeOut();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MintStage");
    }
}
