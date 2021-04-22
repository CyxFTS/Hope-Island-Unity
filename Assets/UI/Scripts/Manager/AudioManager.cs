using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { private set; get; }
    private AudioSource m_audioSource;
    private AudioSource m_ButtonAudio;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        // m_audioSource = GetComponent<AudioSource>();
        // m_audioSource.clip = Resources.Load<AudioClip>(AudioPath.main);
        // m_audioSource.Play();
        // m_audioSource.loop = true;

        GameObject buttonAudio = new GameObject("ButtonAudio");
        buttonAudio.transform.parent=this.transform;
        m_ButtonAudio = buttonAudio.AddComponent<AudioSource>();
        m_ButtonAudio.clip = Resources.Load<AudioClip>(AudioPath.select);
        m_ButtonAudio.playOnAwake = false;
    }

    public void PlayerButtonAudio()
    {
        m_ButtonAudio.Play();
    }
}
public class AudioPath
{
    public const string select = "Audios/select_button_press";
    public const string main = "Audios/main_menu";
}
public static class ButtonExpansion
{
    public static void Add(this Button but, Action action)
    {
        but.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayerButtonAudio();
            action.Invoke();
        });
    }
}

