using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [System.Serializable]
    public class Obstacle
    {
        public string Name;
        public float raridade;
        public float PositionY;
        public GameObject ObstacleGO;
    }

    public List<Obstacle> ObstaclesList;

    //time
    [Header("Time")]
    public float MinGenerationTime;
    public float MaxGenerationTime;
    public float DecreaseGenerationTime; //Valor q o tempo de geracao maximo deminui a cada segundo
    public float MargemTime;

    public int UnlockSky;
    public float InicialDelay;

    private GameObject CurrentObstacle;
    private Vector2 InsPosition;

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

            if(MaxGenerationTime > MinGenerationTime + MargemTime) //Se o tempo max for maior q o tempo min + 0.5s
            {
                MaxGenerationTime -= DecreaseGenerationTime; //Diminuir tempo maximo
            }
        }
    }

    IEnumerator GenerateObstacle() //Gerar obstacles
    {
        yield return new WaitForSeconds(InicialDelay);
        while (true) //loop infinito
        {
            float RandomTime = Random.Range(MinGenerationTime, MaxGenerationTime); //Um tempo aleatorio para gerar
            yield return new WaitForSeconds(RandomTime); //Esperar o tempo aleatorio

            float RandomRarity = Random.Range(0f, 100f);

            //Debug.Log(RandomRarity);

            for (int i = 0; i < ObstaclesList.Count; i++)
            {
                if(ObstaclesList[i].raridade >= RandomRarity)
                {
                    CurrentObstacle = ObstaclesList[i].ObstacleGO;

                    InsPosition = new Vector2(transform.position.x, ObstaclesList[i].PositionY);
                }
            }

            if(UnlockSky > 0 && CurrentObstacle.GetComponent<ObstacleScript>().ObstacleType == "Sky")
            {
                CurrentObstacle = ObstaclesList[0].ObstacleGO;
                InsPosition = new Vector2(transform.position.x, ObstaclesList[0].PositionY);
            }

            UnlockSky--;

            GameObject Ins = Instantiate(CurrentObstacle, InsPosition, transform.rotation);
        }
    }
}
