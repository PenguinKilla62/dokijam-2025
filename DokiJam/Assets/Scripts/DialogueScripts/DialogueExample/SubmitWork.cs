using System;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using Yarn.Unity;


public class SubmitWork : MonoBehaviour
{

    private InMemoryVariableStorage variableStorage;

    // Keep track of GameObject Values
    public SliderScript sliderGameObject;
    public DialogueRunner dialogueRunner;

    private int _redValue = 127;
    private int _greenValue = 0;
    private int _blueValue = 0;

    private int _higherUpCurrentCount = 0;
    public int higherUpMaxCount = 10;
    private bool stopWork = false;

    private enum ColorPick
    {
        Red,
        Green,
        Blue
    };

    public void Start()
    {
        variableStorage = FindFirstObjectByType<InMemoryVariableStorage>();
        var objects = FindObjectsByType<InMemoryVariableStorage>(FindObjectsSortMode.InstanceID);
        foreach (var thing in objects)
        {
            if (thing.name == "Worker Dialogue System")
            {
                variableStorage = thing;
            }
        }
        DetermineNextChange();
        UpdateVariableStorage();
        dialogueRunner.StartDialogue("WorkerStarting");
        dialogueRunner.StartDialogue("WorkerSetMultipleColors");
    }

    public void DetermineNextChange()
    {
        var rnd = new System.Random();
        int pickCount = rnd.Next(1, 3);
        List<ColorPick> pickList = new List<ColorPick> { ColorPick.Red, ColorPick.Green, ColorPick.Blue };
        List<ColorPick> chosenList = new List<ColorPick> { };

        for (var i = 0; i < pickCount; i++)
        {
            var index = rnd.Next(pickList.Count);
            chosenList.Add(pickList[index]);
            pickList.RemoveAt(index);
        }

        foreach (var pick in chosenList)
        {
            switch (pick)
            {
                case ColorPick.Red:
                    _redValue = rnd.Next(0, 255);
                    break;
                case ColorPick.Green:
                    _greenValue = rnd.Next(0, 255);
                    break;
                case ColorPick.Blue:
                    _blueValue = rnd.Next(0, 255);
                    break;
            }
        }

    }

    private bool MeetsCurrentChange()
    {
        bool result = true;

        // if (_redValue != sliderGameObject.RedValue || _greenValue != sliderGameObject.GreenValue || _blueValue != sliderGameObject.BlueValue)
        // {
        //     result = false;
        // }
        // gonna give this a bit more leniency cuz doing this on mouse is just carpal tunnel simulator
        if (Math.Abs(_redValue - sliderGameObject.RedValue) > 10 || Math.Abs(_greenValue - sliderGameObject.GreenValue) > 10 || Math.Abs(_blueValue - sliderGameObject.BlueValue) > 10)
        {
            result = false;
        }

        return result;
    }

    public void UpdateVariableStorage()
    {
        variableStorage.SetValue("$red_amount", _redValue);
        variableStorage.SetValue("$green_amount", _greenValue);
        variableStorage.SetValue("$blue_amount", _blueValue);
    }

    private bool StopWork()
    {
        return _higherUpCurrentCount >= higherUpMaxCount && Math.Abs(sliderGameObject.BlueValue - 127) > 10
            && Math.Abs(sliderGameObject.RedValue - 127) > 10 && Math.Abs(sliderGameObject.GreenValue - 127) > 10;
    }

    public void CheckWork()
    {
        if (MeetsCurrentChange() == false)
        {
            dialogueRunner.StartDialogue("WorkerFailedSelfReview");
            dialogueRunner.StartDialogue("WorkerSetMultipleColors");
        }
        else
        {
            if (!stopWork)
            {
                dialogueRunner.StartDialogue("WorkerHigherUpReviewing");
            }

            if (StopWork())
            {
                stopWork = true;
                dialogueRunner.StartDialogue("WorkerFinished");
            }
            else
            {
                if (_higherUpCurrentCount >= higherUpMaxCount)
                {
                    _redValue = 127;
                    _greenValue = 127;
                    _blueValue = 127;
                }
                else
                {
                    DetermineNextChange();
                    _higherUpCurrentCount += 1;
                }
                UpdateVariableStorage();
                dialogueRunner.StartDialogue("WorkerDisgruntled");
                dialogueRunner.StartDialogue("WorkerSetMultipleColors");
            }
        }
    }
}
