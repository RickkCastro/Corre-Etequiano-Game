using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    [System.Serializable]
    public class Item
    {
        public string Name;
        public float raridade;
        public float PositionY;
        public GameObject ItemGO;
    }

    public List<Item> ItemsList;

    //time
    [Header("Time")]
    public float MinGenerationTime;
    public float MaxGenerationTime;
    public float InicialDelay;

    private GameObject CurrentItem;
    private Vector2 InsPosition;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateItem()); //Gerar obstacles
    }

    IEnumerator GenerateItem() //Gerar obstacles
    {
        yield return new WaitForSeconds(InicialDelay);
        while (true) //loop infinito
        {
            float RandomTime = Random.Range(MinGenerationTime, MaxGenerationTime); //Um tempo aleatorio para gerar
            yield return new WaitForSeconds(RandomTime); //Esperar o tempo aleatorio

            float RandomRarity = Random.Range(0f, 100f);

            //Debug.Log(RandomRarity);

            bool gerarItem = false;
            for (int i = 0; i < ItemsList.Count; i++)
            {
                if (ItemsList[i].raridade >= RandomRarity)
                {
                    CurrentItem = ItemsList[i].ItemGO;
                    InsPosition = new Vector2(transform.position.x, ItemsList[i].PositionY);
                    gerarItem = true;
                }
            }

            if (gerarItem)
            {
                GameObject Ins = Instantiate(CurrentItem, InsPosition, transform.rotation);
                yield return new WaitForSeconds(5f);
            }
        }
    }
}
