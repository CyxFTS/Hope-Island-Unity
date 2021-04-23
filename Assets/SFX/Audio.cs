using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class Audio : MonoBehaviour
{
    public AudioClip audioClip;
    private AudioSource MainAudio;
    // Start is called before the first frame update
    void Start()
    {
        MainAudio = GetComponent<AudioSource>();
        MainAudio.loop = true;
        MainAudio.clip = audioClip;
        if (MainAudio.clip!=null)
        {
            MainAudio.Play();
        }
    }
}
