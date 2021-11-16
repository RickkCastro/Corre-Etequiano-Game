using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject Icon;
    public GameObject TutorialScreen;
    public GameObject CreditsScreen;

    private void Awake()
    {
        Icon = transform.GetChild(1).gameObject;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Icon.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Icon.SetActive(false);
    }

    public void PlayClick()
    {
        TutorialScreen.SetActive(true);
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("GameScene");
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
