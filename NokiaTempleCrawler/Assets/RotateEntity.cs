using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEntity : MonoBehaviour
{
    private float rotationSpeed = 3.0f;

    void Update()
    {
        transform.Rotate(0.0f, rotationSpeed, 0.0f);
    }
}
