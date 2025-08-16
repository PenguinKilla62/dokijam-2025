using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class beginningMonologueAnimator : MonoBehaviour
{
    public Texture[] Textures1;
    public Texture[] Textures2;
    public Texture[] Textures3;
    public Texture[] Textures4;
    public GameObject image;
    private Texture[][] allTextures;
    public int whichScene = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        allTextures = new Texture[][] { Textures1, Textures2, Textures3, Textures4 };
    }

    // Update is called once per frame
    void Update()
    {
        image.GetComponent<RawImage>().texture = allTextures[whichScene][(int)(Time.time * 10) % allTextures[whichScene].Length];
    }

    public void NextScene()
    {
        whichScene += 1;
        if (whichScene >= allTextures.Length)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Office");
        }
    }
}
