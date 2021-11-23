using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGAux : MonoBehaviour
{
    //Script do fundo do menu

    private GameObject BG; //pega fundo origina principal

    private void Awake()
    {
        BG = transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SceneryLine")) //Quando o fundo aux colidir com o fim do cenario
        {
            BG.transform.position = BG.GetComponent<BGMovement>().InicialPos; //Colocar fundo principal no inicio
        }
    }
}
