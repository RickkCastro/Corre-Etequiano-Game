using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    //Selecionar item
    [HideInInspector]
    public List<string> ItemTypeList = new List<string> { "AlcoholAmmu", "LifeMask", "Vaccine" };
    [Dropdown("ItemTypeList")]//input the path of the list
    public string ItemType;

    private void FixedUpdate() //A todo momento
    {
        transform.Translate(Vector2.left * (GameController.instance.CurrentSpeed -2) * Time.deltaTime); //Obstaculo anda para a esquerda
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("finishLine")) //Destruir obstaculo
        {
            Destroy(this.gameObject);
        }
    }
}
