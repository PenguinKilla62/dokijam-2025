using UnityEngine;
using System.Timers;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;

public class AdPopup : MonoBehaviour
{
    public bool ShowSelf = true;
    public float time_seconds = 30;

    public float current_time_seconds_elapsed = 0;

    public int currentCount = 0;
    public int totalCount = 3;

    public Canvas canvas;

    public int topSortingLayerNum = 0;
    public int bottomSortingLayerNum = 0;

    private bool showing = true;

    public void SetContainerElementsVisibility(bool visible)
    {
        Debug.Log("Container: " + visible);
        canvas.overrideSorting = true;
        canvas.sortingOrder = visible ? topSortingLayerNum : bottomSortingLayerNum;
        showing = visible;
    }

    public void ShowContainerImage()
    {
        current_time_seconds_elapsed = 0;
        SetContainerElementsVisibility(true);
    }

    public void HideContainerImage()
    {
        SetContainerElementsVisibility(false);
        currentCount += 1;
    }

    public void Start()
    {
        current_time_seconds_elapsed = 0;
        ShowContainerImage();
    }

    public void Update()
    {
        current_time_seconds_elapsed += Time.deltaTime;
        if (current_time_seconds_elapsed >= time_seconds && currentCount < totalCount && showing == false)
        {
            ShowContainerImage();
        }
    }
}
