using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScreen : MonoBehaviour
{
    //Telas de tutoriais;

    public float TutorialDuration; //Durracao da tela de tutorial
    public GameObject TutorialMobile; //tela de tutorial mobile
    public GameObject TutorialPC; //tela de tutorial pc
    private GameObject Tutorial; //tutorial que vai ser mostrado

    // Start is called before the first frame update
    void Start()
    {
        //Ver se a tela é mobile ou normal
        if (PlayerPrefs.GetInt("IsMobile") == 1)
            Tutorial = TutorialMobile;
        else
            Tutorial = TutorialPC;

        //Se o tutorial ainda n foi mostrado
        if (PlayerPrefs.GetInt("TutorialOff", 0) == 0)
            StartCoroutine(DisableTutorial());
    }

    IEnumerator DisableTutorial()
    {
        Tutorial.SetActive(true);
        yield return new WaitForSeconds(TutorialDuration); //Esperar duracao
        Tutorial.GetComponent<Animator>().SetTrigger("FadeIn"); //Animacao
        yield return new WaitForSeconds(1.2f); //Esperar anim acabar
        Tutorial.SetActive(false); //Desativar tela
        PlayerPrefs.SetInt("TutorialOff", 1); //Mudar no bd
    }
}
