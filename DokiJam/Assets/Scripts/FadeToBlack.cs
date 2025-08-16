using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    private bool isFadingIn = false;
    private bool isFadingOut = false;
    private float fadeSpeed = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FadeIn();
    }

    public void FadeIn()
    {
        // Implement fade-in logic here
        Debug.Log("Fading in...");
        // Example: Use a coroutine to gradually change the alpha of a UI panel or camera overlay
        isFadingIn = true;
        isFadingOut = false;
    }

    public void FadeOut()
    {
        // Implement fade-out logic here
        Debug.Log("Fading out...");
        // Example: Use a coroutine to gradually change the alpha of a UI panel or camera overlay
        isFadingIn = false;
        isFadingOut = true;
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Image>().enabled = true;
        if (isFadingIn)
        {
            if (this.GetComponent<Image>().color.a > 0f)
            {
                // Gradually increase the alpha value
                Color color = this.GetComponent<Image>().color;
                color.a -= Time.deltaTime * fadeSpeed; // fadeSpeed is a float
                this.GetComponent<Image>().color = color;
            }
            else
            {
                isFadingIn = false; // Stop fading when fully opaque
                Debug.Log("Fade in complete.");
            }
        }
        else if(isFadingOut)
        {
            if (this.GetComponent<Image>().color.a < 1f)
            {
                // Gradually decrease the alpha value
                Color color = this.GetComponent<Image>().color;
                color.a += Time.deltaTime * fadeSpeed; // fadeSpeed is a float
                this.GetComponent<Image>().color = color;
            }
            else
            {
                isFadingOut = false; // Start fading out when fully transparent
                Debug.Log("Fade out complete.");
            }
        }
    }
}
