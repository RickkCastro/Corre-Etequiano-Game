using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [System.Serializable]
    public class Obstacle //Valores dos obstacles
    {
        public string Name;
        public float raridade;
        public float PositionY;
        public GameObject ObstacleGO; //Objeto do obstacle
    }

    public List<Obstacle> ObstaclesList; //Lista de obstcles

    //time
    [Header("Time")]
    public float MinGenerationTime;
    public float MaxGenerationTime;
    public float DecreaseGenerationTime; //Valor q o tempo de geracao maximo deminui a cada segundo
    public float MargemTime; //tempo minimo de diferenca a ficar entre o tempo maximo e minimo de geracao

    public int UnlockSky; //Contagem para desbloquear obstacles aereos
    public float InicialDelay; //Delay para comecar a criar obstacles

    private GameObject CurrentObstacle; //Obstacle a ser criado
    private Vector2 InsPosition; //Posicao

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateObstacle()); //Gerar obstacles
        StartCoroutine(Every1Second()); //A cada segundo
    }

    IEnumerator Every1Second() //A cada segundo
    {
        while(true) //Loop infinito
        {
            yield return new WaitForSeconds(1f); //Esperar um segundo

            if(MaxGenerationTime > MinGenerationTime + MargemTime) //Se o tempo max for maior q o tempo min + margem de tempo
            {
                MaxGenerationTime -= DecreaseGenerationTime; //Diminuir tempo maximo
            }
        }
    }

    IEnumerator GenerateObstacle() //Gerar obstacles
    {
        yield return new WaitForSeconds(InicialDelay); //Esperar delay inicial
        while (true) //loop infinito
        {
            float RandomTime = Random.Range(MinGenerationTime, MaxGenerationTime); //Um tempo aleatorio para gerar
            yield return new WaitForSeconds(RandomTime); //Esperar o tempo aleatorio

            float RandomRarity = Random.Range(0f, 100f); //Raridade

            //Debug.Log(RandomRarity);

            for (int i = 0; i < ObstaclesList.Count; i++) //Passar checando a raridade de todos os obstacles ate pegar o masi raro
            {
                if(ObstaclesList[i].raridade >= RandomRarity) 
                {
                    CurrentObstacle = ObstaclesList[i].ObstacleGO; //Pegar obstacle

                    InsPosition = new Vector2(transform.position.x, ObstaclesList[i].PositionY); //Pegar poscicao
                }
            }

            if(UnlockSky > 0 && CurrentObstacle.GetComponent<ObstacleScript>().ObstacleType == "Sky") //Se um obstacle aereo aparecer e o ceu ainda estivar bloqueado
            {
                CurrentObstacle = ObstaclesList[0].ObstacleGO; //Pegar 1 obstacle da lista
                InsPosition = new Vector2(transform.position.x, ObstaclesList[0].PositionY); 
            }

            UnlockSky--; //Diminuir contagem para desbloquear o ceu

            GameObject Ins = Instantiate(CurrentObstacle, InsPosition, transform.rotation); //Criar obstacle
        }
    }
}
