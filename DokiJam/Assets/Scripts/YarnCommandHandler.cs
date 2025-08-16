using UnityEngine;
using Yarn.Unity;
using System.Collections;
using UnityEngine.Events;
using System;

public class YarnCommandHandler : MonoBehaviour
{
    private bool waiting = false;
    private bool seenBoss = false;
    private InMemoryVariableStorage variableStorage;
    private DialogueRunner dialogueRunner;

    public void Start()
    {
        variableStorage = FindFirstObjectByType<InMemoryVariableStorage>();
        dialogueRunner = FindFirstObjectByType<DialogueRunner>();

        dialogueRunner.onNodeStart.AddListener(OnNodeStart);
        dialogueRunner.onNodeComplete.AddListener(OnNodeComplete);
    }

    [YarnCommand("removeActions")]
    public void RemoveActions()
    {
        Debug.Log("Removing actions");
        // grab Doki game object and remove DokiTalk
        GameObject doki = GameObject.Find("Doki");
        if (doki != null)
        {
            DokiActions dokiTalk = doki.GetComponent<DokiActions>();
            if (dokiTalk != null)
            {
                dokiTalk.enabled = false;
                Debug.Log("DokiTalk component disabled");
            }
            else
            {
                Debug.LogWarning("DokiTalk component not found on Doki game object");
            }
        }
        else
        {
            Debug.Log("Doki game object not found");
        }
    }

    [YarnCommand("enableActions")]
    public void EnableActions()
    {
        Debug.Log("Enabling actions");
        // grab Doki game object and enable DokiTalk
        GameObject doki = GameObject.Find("Doki");
        if (doki != null)
        {
            DokiActions dokiTalk = doki.GetComponent<DokiActions>();
            if (dokiTalk != null)
            {
                dokiTalk.enabled = true;
                Debug.Log("DokiTalk component enabled");
            }
            else
            {
                Debug.LogWarning("DokiTalk component not found on Doki game object");
            }
        }
        else
        {
            Debug.Log("Doki game object not found");
        }
    }

    [YarnCommand("goToWork")]
    public void GoToWork()
    {
        Debug.Log("Going to work");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Office");
    }

    [YarnCommand("startWork")]
    public void StartWork()
    {
        Debug.Log("Starting work");
        UnityEngine.SceneManagement.SceneManager.LoadScene("IntroComputer");
    }

    [YarnCommand("finishWork")]
    public void FinishWork()
    {
        Debug.Log("Finished work");
        ComputerChanger computerChanger = FindFirstObjectByType<ComputerChanger>();
        computerChanger.ShutdownEverythingElse();
    }

    [YarnCommand("popups")]
    public void Popups(int whichAd)
    {
        Debug.Log("First popup command called");
        ComputerChanger computerChanger = FindFirstObjectByType<ComputerChanger>();
        computerChanger.DisplayAd(whichAd);
    }

    [YarnCommand("isekai")]
    public void Isekai()
    {
        Debug.Log("Isekai command called");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Isekai");
    }

    [YarnCommand("popupdestroy")]
    public void PopupDestroy()
    {
        Debug.Log("Popup destroy command called");
        ComputerChanger computerChanger = FindFirstObjectByType<ComputerChanger>();
        computerChanger.HideNoButton();
    }

    [YarnCommand("lookAroundConfused")]
    public void LookAroundConfused()
    {
        Debug.Log("Looking around confused");
        GameObject doki = GameObject.Find("Doki");
        if (doki != null)
        {
            DokiActions dokiActions = doki.GetComponent<DokiActions>();
            if (dokiActions != null)
            {
                // doki looks first to the left and then to the right, down, and then back up
                dokiActions.LookAroundConfused();
            }
            else
            {
                Debug.LogWarning("DokiActions component not found on Doki game object");
            }
        }
        else
        {
            Debug.Log("Doki game object not found");
        }
    }

    public void PlayYarn(string yarnFileName)
    {
        if (waiting)
        {
            return;
        }
        var dialogueSystem = FindFirstObjectByType<DialogueRunner>();
        Debug.Log("Playing Yarn file: " + yarnFileName);
        if (dialogueSystem != null)
        {
            if (dialogueSystem.IsDialogueRunning)
            {
                return; // Prevent starting a new dialogue if one is already running
            }
            dialogueSystem.StartDialogue(yarnFileName);
        }
        else
        {
            Debug.LogWarning("DialogueSystem not found in the scene");
        }
    }

    public void MeetGod()
    {
        Debug.Log("chain reaction baybeeeee");
        UnityEngine.SceneManagement.SceneManager.LoadScene("WhiteSpace");
    }

    private void OnNodeStart(string nodeName)
    {
        Debug.Log("Node started: " + nodeName);
        RemoveActions();
        return;
    }

    private void OnNodeComplete(string nodeName)
    {
        EnableActions();
        return;
    }
}
