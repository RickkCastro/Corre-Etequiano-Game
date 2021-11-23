using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //Camera se movimentando no jogo

    public Vector3 InicialPos; //Posicao original 

    private void Awake()
    {
        InicialPos = transform.position; //Pegar posicao original
    }

    // Update is called once per frame
    private void Update() //A todo momento
    {
        transform.Translate(Vector2.right * (GameController.instance.CurrentSpeed /2) * Time.deltaTime); //Camera anda para a direita
    }
}
