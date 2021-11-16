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
        if(ObstacleType == "Sky")
            transform.Translate(Vector2.left * (GameController.instance.CurrentSpeed * 2) * Time.deltaTime); //Obstaculo anda para a esquerda
        else
            transform.Translate(Vector2.left * (GameController.instance.CurrentSpeed) * Time.deltaTime); //Obstaculo anda para a esquerda
    }

    private void Start()
    {
        Destroy(gameObject, 10f); //Destruir objeto
    }

    private void OnTriggerEnter2D(Collider2D collision) //Colisoes
    {
        if (collision.gameObject.CompareTag("Obstacle")) //Se um obstaculo colidir com outro obstaculo, destruir um
        {
            Destroy(collision.gameObject);
        }
    }
}
