using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneryLineScript : MonoBehaviour
{
    //Camera se movimentando no jogo

    private GameObject Cam; //Camera

    private void Start()
    {
        Cam = GameObject.FindGameObjectWithTag("MainCamera"); //Pegar camera
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SceneryLine")) //Quando colidir com a linha do cenario
        {
            Cam.transform.position = Cam.GetComponent<CameraMovement>().InicialPos; //colocar camera na pos inicial
        }
    }
}
