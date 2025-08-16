using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class Data
{
    public string name;
    public int maxBlock;
    public int BPM;
    public int offset;
    public Note[] notes;

}
[Serializable]
public class Note
{
    public int type;
    public int num;
    public int block;
    public int LPB;
}

public class NotesManager : MonoBehaviour
{
    public int noteNum;
    [SerializeField]
    public string songName = "";

    public List<int> LaneNum = new List<int>();
    public List<int> NoteType = new List<int>();
    public List<float> NotesTime = new List<float>();
    public List<GameObject> NotesObj = new List<GameObject>();

    [SerializeField] private float NotesSpeed;
    [SerializeField] GameObject noteObj;

    [SerializeField] GameObject leftNote;
    [SerializeField] GameObject downNote;
    [SerializeField] GameObject upNote;
    [SerializeField] GameObject rightNote;

    [SerializeField] 
    Canvas canvas;

    void OnEnable()
    {
        noteNum = 0;
        //songName = "テスト";
        //Load(songName);
    }

    public void PauseOrUnPauseNotes(bool value)
    {
        
        var doki = GameObject.Find("dokiPlatform");
        var myNotes = doki.GetComponentsInChildren<Notes>();
        foreach (var noteT in myNotes)
        {
            noteT.isPaused = value;
            //Destroy(noteT.gameObject);
        }

        var amalee = GameObject.Find("amaleePlatform");
        var amaNotes = amalee.GetComponentsInChildren<Notes>();
        foreach (var noteT in amaNotes)
        {
            noteT.isPaused = value;
        }

        // if (value)
        // {


        // }
        // else
        // {
        //     foreach (var note in NotesObj)
        //     {
        //         var notesComponent = note.GetComponent<Notes>();

        //         notesComponent.isPaused = value;

        //     }
        // }
    }

    public async Task Load(string SongName)
    {
        Debug.Log(SongName);
        string inputString = Resources.Load<TextAsset>(SongName).ToString();
        Data inputJson = JsonUtility.FromJson<Data>(inputString);

        noteNum = inputJson.notes.Length;

        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            float kankaku = 60 / (inputJson.BPM * (float)inputJson.notes[i].LPB);
            float beatSec = kankaku * (float)inputJson.notes[i].LPB;
            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset + 0.01f;
            NotesTime.Add(time);
            LaneNum.Add(inputJson.notes[i].block);
            NoteType.Add(inputJson.notes[i].type);


            float y = 55f - (NotesTime[i] * NotesSpeed);
            float x = inputJson.notes[i].block * -35 + 50.0f;
            float z = 0.55f;

            Vector3 newPosition = new Vector3(x, y, z);

            var block = inputJson.notes[i].block;
            GameObject newNoteObj;
            if (block == 3)
            {
                newNoteObj = Instantiate(leftNote, newPosition, Quaternion.identity);
            }
            else if (block == 2)
            {
                newNoteObj = Instantiate(downNote, newPosition, Quaternion.identity);
            }
            else if (block == 1)
            {
                newNoteObj = Instantiate(upNote, newPosition, Quaternion.identity);
            }
            else if (block == 0)
            {
                newNoteObj = Instantiate(rightNote, newPosition, Quaternion.identity);
            }
            else
            {
                newNoteObj = Instantiate(noteObj, newPosition, Quaternion.identity);
            }
            newNoteObj.transform.SetParent(canvas.transform, false);
            NotesObj.Add(newNoteObj);
        }
    }
}