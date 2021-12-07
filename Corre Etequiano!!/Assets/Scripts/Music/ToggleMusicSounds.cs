using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMusicSounds : MonoBehaviour
{
    public bool isMusicToggle;
    private Toggle toggle;

    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<Toggle>();

        if (isMusicToggle)
        {
            if (PlayerPrefs.GetInt("MuteMusic", 0) == 1)
                toggle.isOn = false;
            else
                toggle.isOn = true;
        }
        else
        {
            if (PlayerPrefs.GetInt("MuteSounds", 0) == 1)
                toggle.isOn = false;
            else
                toggle.isOn = true;
        }
    }

    public void ChangeMusicState()
    {
        bool Mute = !toggle.isOn;

        if (isMusicToggle)
        {
            if (Mute)
                PlayerPrefs.SetInt("MuteMusic", 1);
            else
                PlayerPrefs.SetInt("MuteMusic", 0);
        }
        else
        {
            if (Mute)
                PlayerPrefs.SetInt("MuteSounds", 1);
            else
                PlayerPrefs.SetInt("MuteSounds", 0);
        }
    }
}
