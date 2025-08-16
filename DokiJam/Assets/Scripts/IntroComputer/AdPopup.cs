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

    public GameObject actualAd;

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
        else if (current_time_seconds_elapsed >= time_seconds && currentCount < totalCount && showing == true)
        {
            // time to spawn more
            // float randX = UnityEngine.Random.Range(-1f, 1f);
            // float randY = UnityEngine.Random.Range(-1f, 1f);
            // var newAd = Instantiate(actualAd, actualAd.transform.position + new Vector3(randX, randY, actualAd.transform.position.z), Quaternion.identity);
            // Transform adTransform = newAd.transform.parent;
            // newAd.transform.SetParent(adTransform);
            // current_time_seconds_elapsed = 0;
        }
    }
}
