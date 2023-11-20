using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameScreens : MonoBehaviour
{
    //Script das telas que aparecem durante o jogo, como pause e gameover

    public GameObject PauseScreen; //Tela de pause
    public GameObject DeathScreen; //Tela de morte
    public GameObject AdPanel; //Panel de anuncio 
    public GameObject LoadingScreen;
    public GameObject ReturnScreen;

    //Sons
    [Header("Sons")]
    public AudioClip sPauseIn; 
    public AudioClip sPauseOut;
    public AudioClip sButtons;

    public void PlayAgain() //Botao de jogar dnv
    {
        GetComponent<AudioSource>().clip = sButtons; //Colocar audio de click
        GetComponent<AudioSource>().Play(); //Executar audio de click
        GameObject.Find("ControlMusic").GetComponent<MusicScript>().RestartMusic(); //Reiniciar musica

        //Resetar valores
        try //tentar achar objeto
        {
            BDManager.instace.ReniciarBd();
        }
        catch
        { //Criar caso n ache
            BDManager bdManager = Instantiate(Resources.Load<GameObject>("DontDestroy/BDManager").GetComponent<BDManager>());
            bdManager.ReniciarBd();
        }

        SceneManager.LoadScene("Scenary1"); //Carregar cenario 1
    }

    public void CallPauseScreen() //Ativar telas
    {
        PauseScreen.SetActive(true); //Ativa objeto tela
        GetInfos();

        GetComponent<AudioSource>().clip = sPauseIn;
        GetComponent<AudioSource>().Play();

        GameController.instance.IsPaused = true; //Pausar jogo
    }

    public void CallDeathScreen()
    {
        DeathScreen.SetActive(true);
        GetInfos();

        GameController.instance.IsPaused = true; //Pausar jogo
    }

    private void GetInfos() //Pegar infos de tempo
    {
        //Ajustar informacoes de tempo
        TextMeshProUGUI TxtCurrentTime = GameObject.FindGameObjectWithTag("TxtCurrentTime").GetComponent<TextMeshProUGUI>(); //texto de Tempo atual
        TextMeshProUGUI TxtBestTime = GameObject.FindGameObjectWithTag("TxtBestTime").GetComponent<TextMeshProUGUI>(); //texto de melhor tempo

        int time = GameController.instance.GameTime; //Tempo atual do jogo

        int timeMinutes = (time / 60); //Definir minutos
        int timeSeconds = time - (60 * timeMinutes); //Definir segundos

        TxtCurrentTime.text = timeMinutes.ToString("00") + ":" + timeSeconds.ToString("00"); //Colocar texto de tempo atual

        int BestTime = GameController.instance.BestTime; //Melhor tempo

        int BestTimeMinutes = (BestTime / 60); //Definir minutos
        int BestTimeSeconds = BestTime - (60 * BestTimeMinutes); //Definir segundos

        TxtBestTime.text = "Melhor Tempo: " + BestTimeMinutes.ToString("00") + ":" + BestTimeSeconds.ToString("00"); //Colocar texto de melhor tempo
    }

    public void ReturnGame() //Despausar jogo
    {
        ReturnScreen.SetActive(true);
        PauseScreen.SetActive(false); //Desativar tela de pause

        //Som
        GetComponent<AudioSource>().clip = sPauseOut;
        GetComponent<AudioSource>().Play();
    }
}
