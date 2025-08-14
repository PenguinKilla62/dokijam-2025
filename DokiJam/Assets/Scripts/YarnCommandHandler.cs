using UnityEngine;
using Yarn.Unity;
using System.Collections;

public class YarnCommandHandler : MonoBehaviour
{
    private bool waiting = false;
    [YarnCommand("removeActions")]
    public void RemoveActions()
    {
        Debug.Log("Removing actions");
        // grab Doki game object and remove DokiTalk
        GameObject doki = GameObject.Find("Doki");
        if (doki != null)
        {
            DokiTalk dokiTalk = doki.GetComponent<DokiTalk>();
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
            Debug.LogWarning("Doki game object not found");
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
            DokiTalk dokiTalk = doki.GetComponent<DokiTalk>();
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
            Debug.LogWarning("Doki game object not found");
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

    public void onDialogueComplete()
    {
        StartCoroutine(WaitForDialogue());
    }

    private IEnumerator WaitForDialogue()
    {
        waiting = true;
        yield return new WaitForSeconds(10f); // Wait for a short duration to ensure dialogue is complete
        waiting = false;
    }
}
