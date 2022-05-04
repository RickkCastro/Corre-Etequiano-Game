using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationItem : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private bool negativeRotation;

    // Update is called once per frame
    void Update()
    {
        if(negativeRotation)
            transform.Rotate(0, 0, - Time.deltaTime * speed, Space.World);
        else
            transform.Rotate(0, 0, Time.deltaTime * speed, Space.World);
    }
}
