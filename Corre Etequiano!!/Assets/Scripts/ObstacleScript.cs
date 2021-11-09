using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public ObstacleGenerator obstacleGenerator; //Gerar do obstaculo

    //Tipo de obstaculo
    [HideInInspector]
    public List<string> ObstacleTypeList = new List<string> { "Common", "Sky" };
    [Dropdown("ObstacleTypeList")]//input the path of the list
    public string ObstacleType;

    private void FixedUpdate() //A todo momento
    {
        transform.Translate(Vector2.left * obstacleGenerator.CurrentSpeed * Time.deltaTime); //Obstaculo anda para a esquerda
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("finishLine")) //Destruir obstaculo
        {
            Destroy(this.gameObject);
        }
    }
}
