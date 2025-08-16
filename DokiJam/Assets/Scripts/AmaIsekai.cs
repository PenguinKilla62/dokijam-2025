using UnityEngine;
using UnityEngine.SceneManagement;

public class AmaIsekai : MonoBehaviour
{
    public BoxCollider2D mintDoorCollider;
    void OnTriggerEnter2D(Collider2D other)
    {
        // has to be the player cuz nothing else moves lmao
        FadeToBlack fadeToBlack = FindFirstObjectByType<FadeToBlack>();
        fadeToBlack.FadeOut();
        UnityEngine.SceneManagement.SceneManager.LoadScene("AmaStage");
    }
}
