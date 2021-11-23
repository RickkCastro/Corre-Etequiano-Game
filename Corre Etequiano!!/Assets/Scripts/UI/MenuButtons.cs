using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    //Script dos bottoes do menu

    public GameObject CreditsScreen; //Tela de credito

    public void PlayClick() //Botao de play
    {
        GetComponent<AudioSource>().Play(); //Executar som de click
        SceneManager.LoadScene("SelectPlayer"); //Carregar cena de selecionar personagem
    }

    public void CreditsClick() //Botao de credito
    {
        GetComponent<AudioSource>().Play(); ////Executar som de click
        CreditsScreen.SetActive(true); //Ativar tela de creditos
    }

    public void SairClick() //Botao de sair da tela de credito
    {
        GetComponent<AudioSource>().Play(); //Executar som de click
        CreditsScreen.SetActive(false); //Desativar tela de credito
    }
}
