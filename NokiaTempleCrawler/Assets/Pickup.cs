using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    NONE,
    BEETLE,
    HEART,
    SCROLL,
    BATTERY
}

public class Pickup : MonoBehaviour
{
    public static event Action<PickupType> OnPickupCollected;

    public PickupType pickupType;

    private float rotationSpeed = 3.0f;

    void Update()
    {
        transform.Rotate(0.0f, rotationSpeed, 0.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnPickupCollected(pickupType);
        Destroy(gameObject);
    }
}
