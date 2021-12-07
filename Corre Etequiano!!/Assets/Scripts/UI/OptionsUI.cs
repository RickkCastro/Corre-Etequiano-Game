using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsUI : MonoBehaviour
{
    //Script pra controlar as acoes ui do menu de opcoes

    public bool isActive;
    public AudioSource audioSource;

    public void OpenCloseOptions()
    {
        audioSource.Play();
        isActive = !isActive;
        gameObject.SetActive(isActive);
    }

    public void ChangeScene(string scene){
        audioSource.Play();

        if(scene != SceneManager.GetActiveScene().name){
            
            if(scene == "Menu"){
                for(int i = 0; i < GameObject.FindGameObjectsWithTag("DontDestroyOnLoad").Length; i++)
                {
                    Destroy(GameObject.FindGameObjectsWithTag("DontDestroyOnLoad")[i]);
                }
            }
            
            SceneManager.LoadScene(scene);
        }
        else{
            OpenCloseOptions();
        }
    }
}
