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

    //Speed
    public float MinSpeed;
    public float MaxSpeed;
    public float SpeedMultiplier; //Valor q a speed aumenta a cada segundo
    public float CurrentSpeed;

    //time
    public float MinGenerationTime;
    public float MaxGenerationTime;
    public float DecreaseGenerationTime; //Valor q o tempo de geracao maximo deminui a cada segundo

    public int UnlockSky;

    private GameObject CurrentObstacle;
    private Vector2 InsPosition;

    // Start is called before the first frame update
    void Start()
    {
        CurrentSpeed = MinSpeed; //Velocidade atual = a minima
        StartCoroutine(GenerateObstacle()); //Gerar obstacles
        StartCoroutine(Every1Second()); //A cada segundo
    }

    IEnumerator Every1Second() //A cada segundo
    {
        while(true) //Loop infinito
        {
            yield return new WaitForSeconds(1f); //Esperar um segundo

            if (CurrentSpeed < MaxSpeed) //Se a velocidade atual for menor q a velocidade maxima
            {
                CurrentSpeed += SpeedMultiplier; //Aumentar velocidade
            }

            if(MaxGenerationTime > MinGenerationTime + 0.5f) //Se o tempo max for maior q o tempo min + 0.5s
            {
                MaxGenerationTime -= DecreaseGenerationTime; //Diminuir tempo maximo
            }
        }
    }

    IEnumerator GenerateObstacle() //Gerar obstacles
    {
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
                Debug.Log("foi");
            }

            UnlockSky--;

            GameObject Ins = Instantiate(CurrentObstacle, InsPosition, transform.rotation);
            Ins.GetComponent<ObstacleScript>().obstacleGenerator = this;
        }
    }
}
