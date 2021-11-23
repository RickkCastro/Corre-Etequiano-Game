using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMovement : MonoBehaviour
{
    //Script do fundo do menu

    public Vector3 InicialPos; //Posicao inicial do fundo
    public float Speed; //velocidade de movimento

    private void Awake()
    {
        InicialPos = transform.position; //Pegar posicao original
    }

    private void Update() //A todo momento
    {
        transform.Translate(Vector2.left * Speed * Time.deltaTime); //fundo anda para a esquerda
    }
}
