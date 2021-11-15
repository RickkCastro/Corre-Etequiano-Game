using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    //Selecionar item
    [HideInInspector]
    public List<string> ItemTypeList = new List<string> { "AlcoholAmmu", "LifeMask", "Vaccine" }; //Lista de tipos de itens
    [Dropdown("ItemTypeList")]//input the path of the list
    public string ItemType; //tipo do item

    private void FixedUpdate() //A todo momento
    {
        transform.Translate(Vector2.left * (GameController.instance.CurrentSpeed +5) * Time.deltaTime); //Item anda para a esquerda
    }

    private void Start()
    {
        Destroy(gameObject, 10f); //Destruir objeto
    }
}
