using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObject : MonoBehaviour
{
    public float degreesX = 0;
    public float degreesY = 0;
    public float degreesZ = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(degreesX, degreesY, degreesZ, Space.Self);
    }
}
