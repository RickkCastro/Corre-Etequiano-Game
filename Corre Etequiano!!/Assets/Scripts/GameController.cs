using System.Collections;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //Script de controle da tela de jogo

    public int GameTime; //Tempo de game
    public int BestTime; //Melhor tempo
    public int MatchesForAd;

    //Speed - velocidade do jogo
    [Header("Speed")]
    public float MinSpeed;
    public float MaxSpeed;
    public float SpeedMultiplier; //Valor q a speed aumenta a cada segundo
    public float CurrentSpeed; //Velocidade atual do jogo

    public static GameController instance;
    public bool IsPaused; 
    public bool IsDeath; //Player morto

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GameTime = PlayerPrefs.GetInt("Time", 0); //Pegar tempo certo
        BestTime = PlayerPrefs.GetInt("BestTime", GameTime); //Pegar melhor tempo
        CurrentSpeed = PlayerPrefs.GetFloat("CurrentSpeed", MinSpeed); //Colocar a velocidade atual como minima no comeco

        SpawnPlaye();

        StartCoroutine(Timer()); //Comecar timer
        StartCoroutine(Every1Second()); //AumentarVelocidade
    }

    public void SpawnPlaye()
    {
        String PlayerName = PlayerPrefs.GetString("PlayerName", "Taylor");
        GameObject PlayerTemplate = GameObject.FindGameObjectWithTag("Player");

        GameObject p = Instantiate(Resources.Load<GameObject>("Skins/" + PlayerName), PlayerTemplate.transform);
        p.transform.parent = PlayerTemplate.transform.parent;
        p.transform.position = PlayerTemplate.transform.position;
        p.transform.localScale = PlayerTemplate.transform.localScale;

        Destroy(PlayerTemplate);
    }

    private void Update() //A todo momento
    {
        if(GameTime > BestTime) //Se o tempo de jogo for maior que o melhor tempo
        {
            BestTime = GameTime;
            PlayerPrefs.SetInt("BestTime", BestTime);
        }

        //Pausar e despausar jogo
        if (Input.GetKeyDown(KeyCode.Escape) && !IsPaused && !IsDeath || Input.GetKeyDown(KeyCode.P) && !IsPaused && !IsDeath) 
        {
            GameScreens gameScreens = GameObject.Find("CanvasGame").GetComponent<GameScreens>();
            gameScreens.CallPauseScreen();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && IsPaused && !IsDeath || Input.GetKeyDown(KeyCode.P) && IsPaused && !IsDeath)
        {
            GameScreens gameScreens = GameObject.Find("CanvasGame").GetComponent<GameScreens>();
            gameScreens.ReturnGame();
        }

        if(IsPaused)
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        CurrentSpeed = 0;
    }

    private void OnApplicationQuit() //Quando o jogo  fechar
    {
        try //tentar achar objeto
        {
            BDManager.instace.ReniciarBd();
        }
        catch
        { //Criar caso n ache
            BDManager bdManager = Instantiate(Resources.Load<GameObject>("DontDestroy/BDManager").GetComponent<BDManager>());
            bdManager.ReniciarBd();
        }
    }

    IEnumerator Timer()
    {
        while (true) //Loop infinito
        {
            if(!IsPaused) //Se nao tiver pausado
            {
                GameTime++; //Aumentar 1s no timer
                PlayerPrefs.SetInt("Time", GameTime);
                yield return new WaitForSeconds(1f); //Esperar 1s
            }
            else
            {
                yield return new WaitForSeconds(0.01f);
            }

        }
        
    }

    //Velocidade
    IEnumerator Every1Second() //A cada segundo
    {
        while (true) //Loop infinito
        {
            if(!IsPaused) //Se nao estiver pausado
            {
                if (CurrentSpeed < MaxSpeed) //Se a velocidade atual for menor q a velocidade maxima
                {
                    CurrentSpeed = PlayerPrefs.GetFloat("CurrentSpeed", CurrentSpeed);
                    CurrentSpeed += SpeedMultiplier; //Aumentar velocidade
                    PlayerPrefs.SetFloat("CurrentSpeed", CurrentSpeed);
                }

                yield return new WaitForSeconds(1f); //Esperar um segundo
            }
            else
            {
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}