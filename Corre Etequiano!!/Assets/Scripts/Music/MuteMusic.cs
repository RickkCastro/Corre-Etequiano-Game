using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteMusic : MonoBehaviour
{
    public bool Mute;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("MuteMusic", 0) == 1)
        {
            Mute = true;
            audioSource.mute = Mute;
        }

        if (PlayerPrefs.GetInt("MuteMusic", 0) == 0)
        {
            Mute = false;
            audioSource.mute = Mute;
        }
    }
}
