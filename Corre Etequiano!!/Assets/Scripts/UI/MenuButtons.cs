using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    //Script dos bottoes do menu

    public void PlayClick() //Botao de play
    {
        SceneManager.LoadScene("ClosetStore"); //Carregar cena de selecionar personagem
    }
}
