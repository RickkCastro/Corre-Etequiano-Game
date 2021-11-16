using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMovement : MonoBehaviour
{
    public Vector3 InicialPos;

    private void Awake()
    {
        InicialPos = transform.position;
    }

    private void FixedUpdate() //A todo momento
    {
        transform.Translate(Vector2.left * (GameController.instance.CurrentSpeed) * Time.deltaTime); //Item anda para a esquerda
    }
}
