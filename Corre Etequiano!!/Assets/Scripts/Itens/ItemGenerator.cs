using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    //Script de gerar itens

    [System.Serializable]
    public class Item //Valores que cada item tem
    {
        public string Name;
        public float raridade;
        public float PositionY;
        public GameObject ItemGO; //Obejto do item
    }

    public List<Item> ItemsList; //Fazer uma lista dos itens

    //time
    [Header("Time")]
    public float MinGenerationTime;
    public float MaxGenerationTime;
    public float DecreaseGenerationTime; //Valor q o tempo de geracao maximo deminui a cada segundo
    public float MargemTime; //tempo minimo de diferenca a ficar entre o tempo maximo e minimo de geracao
    public float InicialDelay; //Tempo que demora para itens comecarem a aparecer

    //privados
    private GameObject CurrentItem; //Item a ser criado
    private Vector2 InsPosition; //Posicao que item sera criado

    void Start() 
    {
        InicialDelay = PlayerPrefs.GetFloat("ItemInicialDelay", InicialDelay); //pegar delay inicial no bd para ficar certo na troca de cenario
        MaxGenerationTime = PlayerPrefs.GetFloat("MaxGTimeItem", MaxGenerationTime);

        StartCoroutine(GenerateItem()); //Gerar itens
        StartCoroutine(Every1Second()); //A cada segundo
    }

    IEnumerator Every1Second() //A cada segundo
    {
        while (true) //Loop infinito
        {
            yield return new WaitForSeconds(1f); //Esperar um segundo

            if(GameController.instance.IsPaused == false)
            {
                if (MaxGenerationTime > MinGenerationTime + MargemTime) //Se o tempo max for maior q o tempo min + margem de tempo
                {
                    MaxGenerationTime -= DecreaseGenerationTime; //Diminuir tempo maximo

                    PlayerPrefs.SetFloat("MaxGTimeItem", MaxGenerationTime);
                }
            }
        }
    }

    IEnumerator GenerateItem() //Gerar itens
    {
        yield return new WaitForSeconds(InicialDelay); //Esperar o delay inicial
        PlayerPrefs.SetFloat("ItemInicialDelay", 0); //Setar no bd o delay inicial como 0

        while (true) //loop infinito
        {
            float RandomTime = Random.Range(MinGenerationTime, MaxGenerationTime); //Um tempo aleatorio para gerar
            yield return new WaitForSeconds(RandomTime); //Esperar o tempo aleatorio

            float RandomRarity = Random.Range(0f, 100f); //Raridade

            //Debug.Log(RandomRarity);

            if(GameController.instance.IsPaused == false)
            {
                bool gerarItem = false; //Se algum item vai ser gerado ou nn
                for (int i = 0; i < ItemsList.Count; i++) //Passar por todos os itens para ver se algum tem a raridade certa para aparecer
                {
                    if (ItemsList[i].raridade >= RandomRarity) //Se tiver a raridade
                    {
                        CurrentItem = ItemsList[i].ItemGO; //Pegar item
                        InsPosition = new Vector2(transform.position.x, ItemsList[i].PositionY); //Posicao
                        gerarItem = true; //Ativar geracao
                    }
                }

                if (gerarItem)
                {
                    GameObject Ins = Instantiate(CurrentItem, InsPosition, transform.rotation); //criar item
                    Ins.transform.parent = this.transform;
                    yield return new WaitForSeconds(MargemTime); //Delay para aparecer outro
                }
            }
        }
    }
}
