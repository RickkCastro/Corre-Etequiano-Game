using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlcoholScript : MonoBehaviour
{
    public float Speed;

    private void FixedUpdate() //A todo momento
    {
        transform.Translate(Vector2.right * Speed * Time.deltaTime); //Obstaculo anda para a esquerda
        Destroy(gameObject, 5f);
    }
}
