using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Vector3 InicialPos;

    private void Awake()
    {
        InicialPos = transform.position;
    }

    // Update is called once per frame
    private void Update() //A todo momento
    {
        transform.Translate(Vector2.right * (GameController.instance.CurrentSpeed /2) * Time.deltaTime); //Item anda para a esquerda
    }
}
