using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileConfig : MonoBehaviour
{
    public bool IsMobile;

    // Start is called before the first frame update
    void Start()
    {
        if (IsMobile)
            PlayerPrefs.SetInt("IsMobile", 1);
        else
            PlayerPrefs.SetInt("IsMobile", 0);
    }
}
