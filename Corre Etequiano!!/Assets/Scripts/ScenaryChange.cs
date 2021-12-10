using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenaryChange : MonoBehaviour
{
    //Script de troca de cenario

    private GameObject player; //Jogdor
    public GameObject FadeOut; //Fade
    public string NextScenary; //Nome do proximo cenario

    [Header("Time")] //tempo
    public int minRandomTime;
    public int maxRandomTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Change()); //Comecar troca
    }

    private IEnumerator Change()
    {
        //esperar tempo aleatorio
        int RandomTime = Random.Range(minRandomTime, maxRandomTime);
        
        int time = 0;

        while(time < RandomTime)
        {
            if(GameController.instance.IsPaused == false) //Se o jogo n estiver pausado 
                time++;

            yield return new WaitForSeconds(1f);
        }

        FadeOut.SetActive(true); //Ativar fade

        player = GameObject.FindGameObjectWithTag("Player"); //pegar objeto do player
        Player playerScript = player.GetComponent<Player>();
        playerScript.immortal = true; //colocar player como imortal

        //trocar cenario
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(NextScenary);
    }
}
