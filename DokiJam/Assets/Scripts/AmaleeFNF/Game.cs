using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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
    public TMP_Text healthText;

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

    private bool gamestopped = false;

    [SerializeField]
    public Canvas restartCanvas;


    public void AddHealth(int value)
    {
        currentHitPoints += value;
        SetHealth();
    }

    public void SetHealth()
    {
        healthText.text = currentHitPoints.ToString();
    }

    public void AddHitCombo()
    {
        hitCombo += 1;
        SetHitCombo();
    }

    public void ClearHitCombo()
    {
        hitCombo = 0;
        SetHitCombo();
    }

    public void SetHitCombo()
    {
        hitComboText.text = hitCombo.ToString();
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

        amaleeNoteManager.PauseOrUnPauseNotes(true);
        dokiNoteManager.PauseOrUnPauseNotes(true);
    }

    public void RestartGame()
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextScene()
    {
        SceneManager.LoadScene("AmaStage");
    }

    async Task LoadNotes()
    {
        var amaleeTask = amaleeNoteManager.Load(amaleeNoteManager.songName);
        var dokiTask = dokiNoteManager.Load(dokiNoteManager.songName);

        List<Task> taskList = new List<Task>() { amaleeTask, dokiTask };
        await Task.WhenAll(taskList);
    }

    async Task StartMusic()
    {
        if (musicSource != null && musicClip != null && amaleeNoteManager != null && dokiNoteManager != null)
        {
            amaleeNoteManager.PauseOrUnPauseNotes(false);
            dokiNoteManager.PauseOrUnPauseNotes(false);

            musicSource.PlayOneShot(musicClip);

            amaleeAudioSource.PlayOneShot(amaleeAudioClip);
            dokiAudioSource.PlayOneShot(dokiAudioClip);

        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async Task Start()
    {
        HideRestart();
        await LoadNotes();
        await StartMusic();
        healthText.text = currentHitPoints.ToString();
        
    }

    void ShowRestart()
    {
        // GameObject gameObject = GameObject.Find("gameovercanvas");
        // gameObject.SetActive(true);
        restartCanvas.enabled = true;
    }

    void HideRestart()
    {
        restartCanvas.enabled = false;
        
        
    }

    void CheckHealth()
    {
        if (currentHitPoints <= 0)
        {
            gamestopped = true;
            StopMusic();
            ShowRestart();
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!gamestopped)
        {
            CheckHealth();
        }
    }
}
