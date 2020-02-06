using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    NONE,
    BEETLE,
    CROSS,
    SCROLL,
    BATTERY
}

public class Pickup : MonoBehaviour
{
    public static event Action<PickupType> OnPickupCollected;

    public PickupType pickupType;

    private void OnTriggerEnter(Collider other)
    {
        OnPickupCollected(pickupType);
        Destroy(gameObject);
    }
}