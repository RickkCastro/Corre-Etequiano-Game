using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    //Script para gerar obstcles

    [System.Serializable]
    public class Obstacle //Valores dos obstacles
    {
        public string Name;
        public float raridade;
        public float PositionY;
        public GameObject ObstacleGO; //Objeto do obstacle
    }

    public List<Obstacle> ObstaclesList; //Lista de obstcles

    [Header("Main")]
    public int UnlockSky; //Contagem para desbloquear obstacles aereos
    public bool IsEnemyGenerator; //Caso seja Gerador de inimigos

    //time
    [Header("Time")]
    public float MinGenerationTime;
    public float MaxGenerationTime;
    public float DecreaseGenerationTime; //Valor q o tempo de geracao maximo deminui a cada segundo
    public float MargemTime; //tempo minimo de diferenca a ficar entre o tempo maximo e minimo de geracao
    public float InicialDelay; //Delay para comecar a criar obstacles

    //Privados
    private GameObject CurrentObstacle; //Obstacle a ser criado
    private Vector2 InsPosition; //Posicao

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateObstacle()); //Gerar obstacles
        StartCoroutine(Every1Second()); //A cada segundo

        if (!IsEnemyGenerator) //Se nao for um gerador de inimigos
        {
            //Variaveis pegas no bd para a troca de cenario continuar com a mesma velocidade e variaveis
            MaxGenerationTime = PlayerPrefs.GetFloat("MaxGTimeCommon", MaxGenerationTime); //Pegar no bd o tempo maximo de geracao
            UnlockSky = PlayerPrefs.GetInt("UnlockSkyObstacles", UnlockSky); //pegar no bd o desbloqueio do ceu
        }
        else //Enemy
        {
            MaxGenerationTime = PlayerPrefs.GetFloat("MaxGTimeEnemy", MaxGenerationTime); //Pegar no bd o tempo maximo de geracao
            InicialDelay = PlayerPrefs.GetFloat("EnemyInicialDeplay", InicialDelay); //Pegar valor do delay inicial para gerar enemy
        }

    }

    IEnumerator Every1Second() //A cada segundo
    {
        while(true) //Loop infinito
        {
            yield return new WaitForSeconds(1f); //Esperar um segundo

            if(MaxGenerationTime > MinGenerationTime + MargemTime) //Se o tempo max for maior q o tempo min + margem de tempo
            {
                MaxGenerationTime -= DecreaseGenerationTime; //Diminuir tempo maximo

                //Salvar valores no bd
                if (!IsEnemyGenerator)
                    PlayerPrefs.SetFloat("MaxGTimeCommon", MaxGenerationTime);
                else //Covid
                    PlayerPrefs.SetFloat("MaxGTimeEnemy", MaxGenerationTime);
            }
        }
    }

    IEnumerator GenerateObstacle() //Gerar obstacles
    {
        yield return new WaitForSeconds(InicialDelay); //Esperar delay inicial

        //Caso seja um gerador de enemy
        if(IsEnemyGenerator)
            PlayerPrefs.SetFloat("EnemyInicialDeplay", 0); //Colocar no bd o delay como 0

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

            if(UnlockSky > 0)
            {
                UnlockSky--; //Diminuir contagem para desbloquear o ceu
                
                //Caso n seja um gerador de enemy
                if (!IsEnemyGenerator)
                    PlayerPrefs.SetInt("UnlockSkyObstacles", UnlockSky); //Colocar valor no bd
            }

            GameObject Ins = Instantiate(CurrentObstacle, InsPosition, transform.rotation); //Criar obstacle
            Ins.transform.parent = this.transform;
        }
    }
}
