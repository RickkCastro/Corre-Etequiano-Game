using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    //Script de controle da tela de jogo

    public int GameTime; //Tempo de game
    public int BestTime; //Melhor tempo

    public GameObject PauseScreen;

    //Speed - velocidade do jogo
    [Header("Speed")]
    public float MinSpeed;
    public float MaxSpeed;
    public float SpeedMultiplier; //Valor q a speed aumenta a cada segundo
    public float CurrentSpeed; //Velocidade atual do jogo

    public static GameController instance;
    private bool PauseOn = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GameTime = PlayerPrefs.GetInt("Time", 0); //Pegar tempo certo
        BestTime = PlayerPrefs.GetInt("BestTime", GameTime); //Pegar melhor tempo
        CurrentSpeed = PlayerPrefs.GetFloat("CurrentSpeed", MinSpeed); //Colocar a velocidade atual como minima no comeco

        StartCoroutine(Timer()); //Comecar timer
        StartCoroutine(Every1Second()); //AumentarVelocidade
    }

    private void Update() //A todo momento
    {
        if(GameTime > BestTime) //Se o tempo de jogo for maior que o melhor tempo
        {
            BestTime = GameTime;
        }

        //Pausar e despausar jogo
        if (Input.GetKeyDown(KeyCode.Escape) && !PauseOn|| Input.GetKeyDown(KeyCode.P) && !PauseOn)
        {
            GameScreens gameScreens = GameObject.Find("CanvasGame").GetComponent<GameScreens>();
            gameScreens.ActivateScreen(PauseScreen);
            PauseOn = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && PauseOn || Input.GetKeyDown(KeyCode.P) && PauseOn)
        {
            GameScreens gameScreens = GameObject.Find("CanvasGame").GetComponent<GameScreens>();
            gameScreens.ReturnGame();
            PauseOn = false;
        }
    }

    private void OnApplicationQuit() //Quando o jogo  fechar
    {
        ReniciarBd();
    }

    public void ReniciarBd() //Apagar tudo
    {
        string PlayerId = PlayerPrefs.GetString("PlayerId");
        BestTime = PlayerPrefs.GetInt("BestTime", GameTime);
        
        PlayerPrefs.DeleteAll();

        //Guardar id do player e melhor tempo
        PlayerPrefs.SetString("PlayerId", PlayerId);
        PlayerPrefs.SetInt("BestTime", BestTime);
    }

    IEnumerator Timer()
    {
        while (true) //Loop infinito
        {
            GameTime++; //Aumentar 1s no timer
            yield return new WaitForSeconds(1f); //Esperar 1s
        }
    }

    //Velocidade
    IEnumerator Every1Second() //A cada segundo
    {
        while (true) //Loop infinito
        {
            yield return new WaitForSeconds(1f); //Esperar um segundo

            if (CurrentSpeed < MaxSpeed) //Se a velocidade atual for menor q a velocidade maxima
            {
                CurrentSpeed += SpeedMultiplier; //Aumentar velocidade
                PlayerPrefs.SetFloat("CurrentSpeed", CurrentSpeed);
            }
        }
    }
}
