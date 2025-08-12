using UnityEngine;
using System.Timers;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;

public class AdPopup : MonoBehaviour
{
    public bool ShowSelf = true;
    public int time_milliseconds = 30000;
    public Timer timer = new Timer();

    public Image containerImage;
    public Button button;
    public List<TMP_Text> text_list;

    public int currentCount = 0;
    public int totalCount = 3;

    public void SetContainerElementsVisibility(bool visible)
    {
        Debug.Log("Container: " + visible);
        containerImage.enabled = visible;
        button.enabled = visible;
        foreach (var text in text_list)
        {
            text.enabled = visible;
        }
    }

    public void ShowContainerImage(object source, ElapsedEventArgs e)
    {
        SetContainerElementsVisibility(true);
    }

    public void HideContainerImage()
    {
        SetContainerElementsVisibility(false);
        currentCount += 1;
        if (currentCount >= totalCount)
        {
            timer.AutoReset = false;
            timer.Enabled = false;
        }
    }

    public void Start()
    {
        timer = new Timer();
        timer.Interval = time_milliseconds; // 30 secs
        timer.Elapsed += ShowContainerImage;
        timer.AutoReset = true;
        timer.Enabled = true;
        timer.Start();

    }

    public void Update()
    {
        
    }
}
