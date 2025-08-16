using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;
using System.Collections.Generic;

public class Game : MonoBehaviour
{

    // The goal is to make it to the end of the song, once there are no more notes on the player side then they win

    [SerializeField]
    public int healthPercentWin = 1;

    [SerializeField]
    public int healthPercentLose = 0;

    [SerializeField]
    public int totalHitPoints = 35;

    [SerializeField]
    public int currentHitPoints = 20;

    public int hitCombo = 0;

    public TMP_Text hitComboText;

    [SerializeField]
    public AudioSource musicSource;

    [SerializeField]
    public AudioClip musicClip;

    [SerializeField]
    public NotesManager amaleeNoteManager;

    [SerializeField]
    public NotesManager dokiNoteManager;

    private bool dokiMuted = false;
    private bool amaleeMuted = false;

    [SerializeField]
    public AudioSource dokiAudioSource;
    [SerializeField]
    public AudioSource amaleeAudioSource;

    [SerializeField]
    public AudioClip dokiAudioClip;
    [SerializeField]
    public AudioClip amaleeAudioClip;



    public void AddHitCombo()
    {
        hitCombo += 1;
    }

    public void ClearHitCombo()
    {
        hitCombo = 0;
    }

    public void MuteUnmuteDoki(bool value)
    {
        dokiMuted = value;
        dokiAudioSource.mute = value;
    }

    public void MuteUnmuteAmalee(bool value)
    {
        amaleeMuted = value;
        amaleeAudioSource.mute = value;
    }

    public void StopMusic()
    {
        dokiAudioSource.Stop();
        amaleeAudioSource.Stop();
        musicSource.Stop();
    }

    async Task StartMusic()
    {
        if (musicSource != null && musicClip != null && amaleeNoteManager != null && dokiNoteManager != null)
        {
            var amaleeTask = amaleeNoteManager.Load(amaleeNoteManager.songName);
            var dokiTask = dokiNoteManager.Load(dokiNoteManager.songName);

            List<Task> taskList = new List<Task>(){ amaleeTask, dokiTask };
            await Task.WhenAll(taskList);

            musicSource.PlayOneShot(musicClip);

            amaleeAudioSource.PlayOneShot(amaleeAudioClip);
            dokiAudioSource.PlayOneShot(dokiAudioClip);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async Task Start()
    {
        await StartMusic();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
