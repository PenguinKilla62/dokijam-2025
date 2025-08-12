using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

[System.Serializable]
public class CharacterVoice
{
    public string name;

    public Sprite sprite;
    public AudioSource audioSource;

    public AudioClip audioClip;
}