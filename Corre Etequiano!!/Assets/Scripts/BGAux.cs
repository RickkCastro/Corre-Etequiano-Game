using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGAux : MonoBehaviour
{
    private GameObject BG;

    private void Awake()
    {
        BG = transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SceneryLine"))
        {
            BG.transform.position = BG.GetComponent<BGMovement>().InicialPos;
        }
    }
}
