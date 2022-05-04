using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    //Script de todos os obstacles gerados

    //Tipo de obstaculo
    [HideInInspector]
    public List<string> ObstacleTypeList = new List<string> { "Common", "Sky", "Enemy", "BossAtack" };
    [Dropdown("ObstacleTypeList")]//input the path of the list
    public string ObstacleType;

    private void Update() //A todo momento
    {
        //transform.Translate(Vector2.left * (GameController.instance.CurrentSpeed) * Time.deltaTime); //Obstaculo anda para a esquerda
        transform.position = new Vector2(transform.position.x - GameController.instance.CurrentSpeed * Time.deltaTime, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision) //Colisoes
    {
        if(collision.tag == "finishLine")
        {
            Destroy(gameObject);
        }

        if (collision.tag == "Obstacle" && ObstacleType == "BossAtack")
            Destroy(collision.gameObject);

        //if(collision.tag == "Alcohol" && ObstacleType == "BossAtack")
        //{
        //    Destroy(gameObject);
        //    Destroy(collision.gameObject);
        //}
    }
}
