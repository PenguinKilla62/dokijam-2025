using UnityEngine;
using UnityEngine.UI;

public class WhiteSpaceHandler : MonoBehaviour
{
    public Sprite[] sprites;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeGoddessHappy()
    {
        GetComponent<Image>().sprite = sprites[0];
    }

    public void ChangeGoddessLooking()
    {
        GetComponent<Image>().sprite = sprites[1];
    }

    public void ChangeGoddessTeehee()
    {
        GetComponent<Image>().sprite = sprites[2];
    }

    public void ChangeGoddessEyesClosed()
    {
        GetComponent<Image>().sprite = sprites[3];
    }

    public void ChangeGoddessPortalOpen()
    {
        GetComponent<Image>().sprite = sprites[4];
    }

    public void ChangeGoddessPortalOpenSolemn()
    {
        GetComponent<Image>().sprite = sprites[5];
    }
}
