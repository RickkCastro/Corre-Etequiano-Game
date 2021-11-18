using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneryLineScript : MonoBehaviour
{
    public GameObject Cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SceneryLine"))
        {
            Cam.transform.position = Cam.GetComponent<CameraMovement>().InicialPos;
        }
    }
}
