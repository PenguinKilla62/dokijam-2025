using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

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

    void AddHitCombo()
    {
        hitCombo += 1;
    }

    void ClearHitCombo()
    {
        hitCombo = 0;
    }

    async Task StartMusic()
    {
        if (musicSource != null && musicClip != null && amaleeNoteManager != null && dokiNoteManager != null)
        {
            amaleeNoteManager.Load(amaleeNoteManager.songName);
            dokiNoteManager.Load(dokiNoteManager.songName);

            musicSource.PlayOneShot(musicClip);
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
