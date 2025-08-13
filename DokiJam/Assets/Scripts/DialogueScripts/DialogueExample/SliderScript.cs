using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    //Values
    public int RedValue;
    public int BlueValue;
    public int GreenValue;

    //TMP_Text
    public TMP_Text sliderRedText;
    public TMP_Text sliderBlueText;
    public TMP_Text sliderGreenText;

    //Slider
    public Slider redSlider;
    public Slider blueSlider;
    public Slider greenSlider;

    public Image image;

    private void changeImageColor()
    {
        image.color = new Color32((byte)RedValue, (byte)GreenValue, (byte)BlueValue, 255);
    }

    public void ChangeRedValue(float newValue)
    {
        RedValue = (int)newValue;
        sliderRedText.text = $"{RedValue}";

        changeImageColor();
    }

    public void ChangeBlueValue(float newValue)
    {
        BlueValue = (int)newValue;
        sliderBlueText.text = $"{BlueValue}";

        changeImageColor();
    }

    public void ChangeGreenValue(float newValue)
    {
        GreenValue = (int)newValue;
        sliderGreenText.text = $"{GreenValue}";

        changeImageColor();
    }

    
}
