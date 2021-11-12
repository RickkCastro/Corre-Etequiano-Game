using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    //Tipo de obstaculo
    [HideInInspector]
    public List<string> ObstacleTypeList = new List<string> { "Common", "Sky", "Covid-19" };
    [Dropdown("ObstacleTypeList")]//input the path of the list
    public string ObstacleType;

    private void FixedUpdate() //A todo momento
    {
        transform.Translate(Vector2.left * (GameController.instance.CurrentSpeed) * Time.deltaTime); //Obstaculo anda para a esquerda
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("finishLine")) //Destruir obstaculo
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Alcohol"))
        {
            if(ObstacleType == "Covid-19")
            {
                Destroy(gameObject);
                Destroy(collision.gameObject);
            }
        }
    }
}
