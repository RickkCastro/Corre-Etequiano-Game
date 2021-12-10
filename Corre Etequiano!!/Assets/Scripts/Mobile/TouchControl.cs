using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControl : MonoBehaviour
{
    // Update is called once per frame
    private void OnEnable()
    {
        if(PlayerPrefs.GetInt("IsMobile", 0) == 1){
            gameObject.SetActive(true);
        }
        else{
            gameObject.SetActive(false);
        }
    }
}
