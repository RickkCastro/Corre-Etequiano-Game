using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    //Script de todos os obstacles gerados

    //Tipo de obstaculo
    [HideInInspector]
    public List<string> ObstacleTypeList = new List<string> { "Common", "Sky", "Enemy" };
    [Dropdown("ObstacleTypeList")]//input the path of the list
    public string ObstacleType;

    private void Update() //A todo momento
    {
        transform.Translate(Vector2.left * (GameController.instance.CurrentSpeed) * Time.deltaTime); //Obstaculo anda para a esquerda
    }

    private void Start()
    {
        Destroy(gameObject, 15f); //Destruir objeto
    }

    private void OnTriggerEnter2D(Collider2D collision) //Colisoes
    {
        if (collision.gameObject.CompareTag("Obstacle")) //Se um obstaculo colidir com outro obstaculo, destruir um
        {
            Destroy(collision.gameObject);
        }
    }
}
