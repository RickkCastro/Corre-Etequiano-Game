using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlcolholScript : MonoBehaviour
{
    public float Speed;

    private void FixedUpdate() //A todo momento
    {
        transform.Translate(Vector2.right * Speed * Time.deltaTime); //Item anda para a esquerda
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out ObstacleScript obstacleScript))
        {
            if (obstacleScript.ObstacleType == "Covid-19")
            {
                //Destruir os dois
                Destroy(gameObject, 5f);
                Destroy(collision.gameObject);
            }
        }
    }
}
