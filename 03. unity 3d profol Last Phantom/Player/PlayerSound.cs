using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour {

    public string[] nameList;
    public AudioSource[] audioList;

    Dictionary<string, AudioSource> soundList;

    private static PlayerSound playSoundManager;
    public static PlayerSound playSoundManagerCall() { return playSoundManager; }

    private void Awake()
    {
        playSoundManager = this;
    }

    private void OnDestroy()
    {
        playSoundManager = null;
    }
    private void Start()
    {
        soundList = new Dictionary<string, AudioSource>();

        for(int i=0; i<nameList.Length;i++)
        {
            soundList.Add(nameList[i], audioList[i]);
        }
    }

    public void PlayAudio(string name,bool forcePlay,float time)
    {
        StartCoroutine( PlayAudioCoroutine(name, forcePlay, time));
    }

    IEnumerator PlayAudioCoroutine(string name, bool forcePlay, float time)
    {
        yield return new WaitForSeconds(time);
        if (soundList.ContainsKey(name))
        {
            AudioSource playSound = soundList[name];
            if (forcePlay)
            {
                playSound.Play();
            }
            else
            {
                if (!playSound.isPlaying) playSound.Play();
            }
        }
        yield return null;
    }
}
