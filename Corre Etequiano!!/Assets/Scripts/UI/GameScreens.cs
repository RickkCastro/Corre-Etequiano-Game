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
        Time.timeScale = 1; //despausar jogo

        GetComponent<AudioSource>().clip = sButtons; //Colocar audio de click
        GetComponent<AudioSource>().Play(); //Executar audio de click
        GameObject.Find("ControlMusic").GetComponent<MusicScript>().RestartMusic(); //Reiniciar musica

        SceneManager.LoadScene("Scenary1"); //Carregar cenario 1
    }

    public void GoToMenu() //Botao de voltar ao menu
    {
        Time.timeScale = 1; //Despausar jogo
        BDManager.instace.ReniciarBd();
        GetComponent<AudioSource>().clip = sButtons; //Colocar audio de click
        GetComponent<AudioSource>().Play(); //Executar audio de click

        for(int i = 0; i < GameObject.FindGameObjectsWithTag("DontDestroyOnLoad").Length; i++)
        {
            Destroy(GameObject.FindGameObjectsWithTag("DontDestroyOnLoad")[i]);
        }

        SceneManager.LoadScene("Menu"); //Carregar menu
    }

    public void ActivateScreen(GameObject Screen) //Ativar telas
    {
        Screen.SetActive(true); //Ativa objeto tela

        if(Screen == PauseScreen) //Se for a tela de pause
        {
            //Som de pausein
            GetComponent<AudioSource>().clip = sPauseIn;
            GetComponent<AudioSource>().Play();
        }

        if (Screen == DeathScreen) //Se for a tela de pause
        {
            if(PlayerPrefs.GetInt("Reborn", 0) == 1)
            {
                AdPanel.SetActive(false);
                //Resetar valores
                BDManager.instace.ReniciarBd();
            }
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


    //Ads
    public void Reborn() //Renascer
    {
        PlayerPrefs.SetInt("Reborn", 1);

        LoadingScreen.SetActive(false);
        AdPanel.SetActive(false);
        DeathScreen.SetActive(false);
        GameObject.Find("ControlMusic").GetComponent<MusicScript>().BGM.volume = 0.5f;

        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.Life = 3;
        PlayerPrefs.SetInt("PlayerLife", player.Life);
        GameUI.instance.UpdateLife(player.Life);
        GameUI.instance.UpdateAlcohol(player.AlcoholAmmu);

        GetComponent<AudioSource>().clip = sPauseIn;
        GetComponent<AudioSource>().Play();

        Time.timeScale = 1;
    }

    public void BtCloseNoAd()
    {
        AdPanel.SetActive(false);

        //Resetar valores
        BDManager.instace.ReniciarBd();
    }

    public void BtWatchAd()
    {
        LoadingScreen.SetActive(true);
        MonetizationManager.Instance.ShowRewarded("Reborn");
    }

    public void WatchAd()
    {
        LoadingScreen.SetActive(true);
        MonetizationManager.Instance.ShowInterstitial();
        LoadingScreen.SetActive(false);
    }
}
