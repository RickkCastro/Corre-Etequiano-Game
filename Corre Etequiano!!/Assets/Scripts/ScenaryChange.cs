using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class ScenaryChange : MonoBehaviour
{
    //Script de troca de cenario

    private GameObject player; //Jogdor
    public GameObject FadeOut; //Fade

    [Header("Time")] //tempo
    public int minRandomTime;
    public int maxRandomTime;

    [SerializeField]
    private List<Scenary> scenarys;

    private string NextScenary; //Nome do proximo cenario

    [System.Serializable]
    public class Scenary
    {
        public string Name;
        public int Chance;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeCountdown()); //Comecar troca
    }

    private void RandomizeScenary()
    {
        NextScenary = SceneManager.GetActiveScene().name;
        while (NextScenary == SceneManager.GetActiveScene().name)
        {
            int RamdomNum = Random.Range(0, 100);

            for(int i = 0; i < scenarys.Count; i++)
            {
                if(RamdomNum <= scenarys[i].Chance)
                {
                    NextScenary = scenarys[i].Name;
                }
            }
        }
    }

    public void Change()
    {
        StartCoroutine(ChangeEnumerator());
    }

    private IEnumerator ChangeEnumerator() //Mudar cenario
    {
        RandomizeScenary();
        FadeOut.SetActive(true); //Ativar fade

        player = GameObject.FindGameObjectWithTag("Player"); //pegar objeto do player
        Player playerScript = player.GetComponent<Player>();
        playerScript.immortal = true; //colocar player como imortal

        //trocar cenario
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(NextScenary);
    }

    private IEnumerator ChangeCountdown() //contagem de tempo aleatorio para mudar cenario
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

        Change();
    }
}
