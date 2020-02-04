using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SignalStrength
{
    STRONG,
    GOOD,
    OKAY,
    POOR,
    NONE
}

public class SignalReception : MonoBehaviour
{
    public static event Action<SignalStrength> OnSignalUpdated;

    [Header("Signal Distances")]
    [SerializeField] private float strongSignalDistance = 2.0f;
    [SerializeField] private float goodSignalDistance = 4.0f;
    [SerializeField] private float okaySignalDistance = 6.0f;
    [SerializeField] private float poorSignalDistance = 8.0f;

    private SignalStrength currentStrength = SignalStrength.STRONG;

    private float currentReceptionValue = 0.0f;

    private void Update()
    {
        CheckReception();
    }

    private void CheckReception()
    {
        SignalStrength previousStrength = currentStrength;

        currentReceptionValue = Vector3.Distance(Vector3.zero, transform.position);

        if (currentReceptionValue < strongSignalDistance)
        {
            currentStrength = SignalStrength.STRONG;
        }
        else if (currentReceptionValue < goodSignalDistance)
        {
            currentStrength = SignalStrength.GOOD;
        }
        else if (currentReceptionValue < okaySignalDistance)
        {
            currentStrength = SignalStrength.OKAY;
        }
        else if (currentReceptionValue < poorSignalDistance)
        {
            currentStrength = SignalStrength.POOR;
        }
        else
        {
            currentStrength = SignalStrength.NONE;
        }

        if (previousStrength != currentStrength)
        {
            OnSignalUpdated(currentStrength);
        }
    }
}
