using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Yarn.Unity;

public class CustomVoiceOverPresenter : DialoguePresenterBase
{
    // The AudioSource component that will play the voiceover audio clips
    public AudioSource audioSource;

    // A reference to the DialogueRunner for controlling dialogue flow
    public DialogueRunner dialogueRunner;

    public AudioClip audioClip;


    public override async YarnTask RunLineAsync(LocalizedLine line, LineCancellationToken token)
    {
        // Check if there is an audio clip associated with this line
        if (audioClip != null)
        {
            // Stop any existing voiceover playing
            var words = line.Text.Text.Split(' ');
            foreach (var word in words)
            {
                if (!string.IsNullOrEmpty(word))
                {
                    audioSource.Stop();

                    // Play the audio clip
                    audioSource.PlayOneShot(audioClip);

                    await Task.Delay(100);
                }
            }
        }

        // Signal that the voiceover (or at least the audio playing aspect) is finished
        // (You might want to add a delay here based on the audio clip's length)
        return;
    }

    public override YarnTask<DialogueOption> RunOptionsAsync(DialogueOption[] dialogueOptions, CancellationToken cancellationToken)
    {
        return DialogueRunner.NoOptionSelected;
    }

    public override YarnTask OnDialogueStartedAsync()
    {
        return YarnTask.CompletedTask;
    }

    public override YarnTask OnDialogueCompleteAsync()
    {
        audioSource.Stop();
        return YarnTask.CompletedTask;
    }
}