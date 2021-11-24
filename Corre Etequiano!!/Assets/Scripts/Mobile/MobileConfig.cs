using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileConfig : MonoBehaviour
{
    public bool IsMobile; //Caso seja mobile = true

    // Start is called before the first frame update
    void Start()
    {
        //Colocar no bd se é mobile ou nn
        if (IsMobile)
            PlayerPrefs.SetInt("IsMobile", 1);
        else
            PlayerPrefs.SetInt("IsMobile", 0);
    }
}
