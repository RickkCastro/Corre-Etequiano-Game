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
        if(PlayerPrefs.GetInt("Reborn", 0) == 1)
        {
            AdPanel.SetActive(false);
            DeathScreen.SetActive(true);
            GetInfos();
        }
        else{
            AdPanel.SetActive(true);
        }

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

        ReturnScreen.SetActive(true);
    }

    public void BtCloseNoAd()
    {
        //Som
        GetComponent<AudioSource>().clip = sButtons; //Colocar audio de click
        GetComponent<AudioSource>().Play(); //Executar audio de click

        AdPanel.SetActive(false);
        DeathScreen.SetActive(true);
        GetInfos();
    }

    public void WatchAd()
    {
        if(MonetizationManager.Instance.AdsOff == false) //se os ads n estiverem desativados 
        {
            //som
            GetComponent<AudioSource>().clip = sButtons; //Colocar audio de click
            GetComponent<AudioSource>().Play(); //Executar audio de click

            LoadingScreen.SetActive(true);

            try //tentar achar objeto
            {
                MonetizationManager.Instance.ShowRewarded("Reborn");
            }
            catch
            { //Criar caso n ache
                MonetizationManager monetizationManager = Instantiate(Resources.Load<GameObject>("DontDestroy/MonetizationManager").GetComponent<MonetizationManager>());
                monetizationManager.ShowRewarded("Reborn");
            }
        }
        else
        {
            Reborn();
        }
    }

    public void WatchAdInterstitial()
    {
        if(MonetizationManager.Instance.AdsOff == false)
        {
            LoadingScreen.SetActive(true);

            try //tentar achar objeto
            {
                MonetizationManager.Instance.ShowInterstitial();
            }
            catch
            { //Criar caso n ache
                MonetizationManager monetizationManager = Instantiate(Resources.Load<GameObject>("DontDestroy/MonetizationManager").GetComponent<MonetizationManager>());
                MonetizationManager.Instance.ShowInterstitial();
            }

            LoadingScreen.SetActive(false);
        }
    }
}
