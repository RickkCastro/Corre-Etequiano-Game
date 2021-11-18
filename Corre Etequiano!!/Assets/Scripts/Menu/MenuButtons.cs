using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public GameObject CreditsScreen;

    public void PlayClick()
    {
        SceneManager.LoadScene("SelectPlayer");       
    }

    public void CreditsClick()
    {
        CreditsScreen.SetActive(true);
    }

    public void SairClick()
    {
        SceneManager.LoadScene("Menu");
    }
}
