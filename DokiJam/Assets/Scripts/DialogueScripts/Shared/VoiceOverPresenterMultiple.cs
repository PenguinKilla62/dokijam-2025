using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Yarn.Unity;

public class VoiceOverPresenterMultiple : DialoguePresenterBase
{

    // A reference to the DialogueRunner for controlling dialogue flow
    public DialogueRunner dialogueRunner;

    public UnityEngine.UI.Image characterImage;

    public List<CharacterVoice> characterVoices;

    public override async YarnTask RunLineAsync(LocalizedLine line, LineCancellationToken token)
    {
        // Check if there is a character

        CharacterVoice? foundCharacterVoice = characterVoices.FirstOrDefault<CharacterVoice>(characterVoice => characterVoice.name == line.CharacterName);

        if (foundCharacterVoice != null)
        {

            if (foundCharacterVoice.sprite != null)
            {
                characterImage.sprite = foundCharacterVoice.sprite;
                characterImage.enabled = true;
            }

            if (foundCharacterVoice.audioClip != null)
            {
                // Stop any existing voiceover playing
                var words = line.Text.Text.Split(' ');
                foreach (var word in words)
                {
                    if (!string.IsNullOrEmpty(word))
                    {
                        foundCharacterVoice.audioSource.Stop();

                        // Play the audio clip
                        foundCharacterVoice.audioSource.PlayOneShot(foundCharacterVoice.audioClip);

                        await Task.Delay(100);
                    }
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
        if (characterVoices != null && characterVoices.Count > 0)
        {
            foreach (var characterVoice in characterVoices)
            {
                characterVoice.audioSource.Stop();
            }
        }
        characterImage.enabled = false;
        return YarnTask.CompletedTask;
    }
}