using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 引数に使用したいサウンドをAudioClip型で入れる
    /// </summary>
    /// <param name="sound"></param>
    public void PlaySE(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }
}
