using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public DialogueRunner dialogueRunner;

    public string startNodeName;

    public void StartYarnDialogue()
    {
        if (dialogueRunner != null && !string.IsNullOrEmpty(startNodeName))
        {
            dialogueRunner.StartDialogue(startNodeName);
        }
        else
        {
            Debug.LogWarning("DialogueRunner or Start Node Name not set for ButtonScript");
        }
    }
}
