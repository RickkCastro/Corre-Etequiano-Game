using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI TxtTime; //Texto que mostra o tempo
    public int time; //tempo

    //Speed - velocidade do jogo
    [Header("Speed")]
    public float MinSpeed;
    public float MaxSpeed;
    public float SpeedMultiplier; //Valor q a speed aumenta a cada segundo
    public float CurrentSpeed;

    public static GameController instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CurrentSpeed = MinSpeed;

        StartCoroutine(Timer()); //Comecar timer
        StartCoroutine(Every1Second()); //AumentarVelocidade
    }

    IEnumerator Timer()
    {
        //Zerar tempo
        int timeSeconds = 0;
        int timeMinutes = 0;

        while (true) //Loop infinito
        {
            yield return new WaitForSeconds(1f); //Esperar 1s
            time++; //Aumentar 1s no timer
            timeMinutes = (time / 60); //Definir minutos
            timeSeconds = time - (60 * timeMinutes); //Definir segundos

            TxtTime.text = "Time: " + timeMinutes.ToString("00") + ":" + timeSeconds.ToString("00"); //Colocar texto
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
            }
        }
    }
}
