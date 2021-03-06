using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    //Script dos itens gerados (movimento)

    //Selecionar item
    [HideInInspector]
    public List<string> ItemTypeList = new List<string> { "AlcoholAmmu", "LifeMask", "Vaccine" }; //Lista de tipos de itens
    [Dropdown("ItemTypeList")]//input the path of the list
    public string ItemType; //tipo do item

    private void Update() //A todo momento
    {
        transform.Translate(Vector2.left * (GameController.instance.CurrentSpeed) * Time.deltaTime); //Item anda para a esquerda
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "finishLine")
        {
            Destroy(gameObject);
        }
    }
}
