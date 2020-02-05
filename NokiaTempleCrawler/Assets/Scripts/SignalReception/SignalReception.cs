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

    public static Action<Vector3> OnLocationUpdated;

    [Header("Signal Distances")]
    [SerializeField] private float strongSignalDistance = 2.0f;
    [SerializeField] private float goodSignalDistance = 4.0f;
    [SerializeField] private float okaySignalDistance = 6.0f;
    [SerializeField] private float poorSignalDistance = 8.0f;

    private SignalStrength currentStrength = SignalStrength.STRONG;

    private float currentReceptionValue = 0.0f;

    private Vector3 signalLocation = Vector3.zero;

    private void OnEnable()
    {
        OnLocationUpdated += UpdateLocation;
    }

    private void OnDisable()
    {
        OnLocationUpdated -= UpdateLocation;
    }

    private void Update()
    {
        CheckReception();
    }

    private void CheckReception()
    {
        SignalStrength previousStrength = currentStrength;

        currentReceptionValue = Vector3.Distance(signalLocation, transform.position);

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

    private void UpdateLocation(Vector3 newLocation)
    {
        Debug.Log("Signal Location changed to " + newLocation.ToString());

        signalLocation = newLocation;
    }
}
