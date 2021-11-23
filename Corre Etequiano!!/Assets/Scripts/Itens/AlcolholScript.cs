using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlcolholScript : MonoBehaviour
{
    //Script do tiro de alcool

    public float Speed; //Velocidade do tiro

    private void Update() //A todo momento
    {
        transform.Translate(Vector2.right * Speed * Time.deltaTime); //tiro anda para a direita
    }

    private void Start()
    {
        Destroy(gameObject, 5f); //Destruir tiro apos 5s
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out ObstacleScript obstacleScript)) //Se o objeto que o tiro colidiu tem o script de obstacle
        {
            if (obstacleScript.ObstacleType == "Enemy") //se o tipo de obstacle for inimigo
            {
                //Destruir os dois
                Destroy(gameObject);
                Destroy(collision.gameObject);
            }
        }
    }
}
