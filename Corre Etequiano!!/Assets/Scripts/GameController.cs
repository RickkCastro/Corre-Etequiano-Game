using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI TxtTime; //Texto que mostra o tempo
    public int time; //tempo

    private void Start()
    {
        StartCoroutine(Timer()); //Comecar timer
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
}
