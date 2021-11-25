using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameScreens : MonoBehaviour
{
    //Script das telas que aparecem durante o jogo, como pause e gameover

    public GameObject PauseScreen; //Tela de pause

    //Sons
    [Header("Sons")]
    public AudioClip sPauseIn; 
    public AudioClip sPauseOut;
    public AudioClip sButtons;

    public void PlayAgain() //Botao de jogar dnv
    {
        Time.timeScale = 1; //despausar jogo
        GetComponent<AudioSource>().clip = sButtons; //Colocar audio de click
        GetComponent<AudioSource>().Play(); //Executar audio de click
        GameObject.Find("ControlMusic").GetComponent<MusicScript>().RestartMusic();

        SceneManager.LoadScene("Scenary1"); //Carregar cenario 1
    }

    public void GoToMenu() //Botao de voltar ao menu
    {
        Time.timeScale = 1; //Despausar jogo
        GameController.instance.ReniciarBd();
        GetComponent<AudioSource>().clip = sButtons; //Colocar audio de click
        GetComponent<AudioSource>().Play(); //Executar audio de click

        if (GameObject.Find("BGM")) //Procurar objeto BGM é destruir para n bugar no menu
            Destroy(GameObject.Find("BGM"));

        SceneManager.LoadScene("Menu"); //Carregar menu
    }

    public void ActivateScreen(GameObject Screen) //Ativar telas
    {
        Screen.SetActive(true); //Ativa objeto tela
        if(Screen.name == "PauseScreen") //Se for a tela de pause
        {
            //Som de pausein
            GetComponent<AudioSource>().clip = sPauseIn;
            GetComponent<AudioSource>().Play();
        }

        //Ajustar informacoes de tempo
        TextMeshProUGUI TxtCurrentTime = Screen.transform.GetChild(2).GetComponent<TextMeshProUGUI>(); //texto de Tempo atual
        TextMeshProUGUI TxtBestTime = Screen.transform.GetChild(1).GetComponent<TextMeshProUGUI>(); //texto de melhor tempo

        int time = GameController.instance.GameTime; //Tempo atual do jogo

        int timeMinutes = (time / 60); //Definir minutos
        int timeSeconds = time - (60 * timeMinutes); //Definir segundos

        TxtCurrentTime.text = "Tempo Atual: " + timeMinutes.ToString("00") + ":" + timeSeconds.ToString("00"); //Colocar texto de tempo atual

        int BestTime = GameController.instance.BestTime; //Melhor tempo

        int BestTimeMinutes = (BestTime / 60); //Definir minutos
        int BestTimeSeconds = BestTime - (60 * BestTimeMinutes); //Definir segundos

        TxtBestTime.text = "Melhor Tempo: " + BestTimeMinutes.ToString("00") + ":" + BestTimeSeconds.ToString("00"); //Colocar texto de melhor tempo

        Time.timeScale = 0; //Pausar jogo
    }

    public void ReturnGame() //Despausar jogo
    {
        Time.timeScale = 1; //Despausar jogo
        PauseScreen.SetActive(false); //Desativar tela de pause

        //Som
        GetComponent<AudioSource>().clip = sPauseOut;
        GetComponent<AudioSource>().Play();
    }
}
