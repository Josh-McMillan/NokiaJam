using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BatteryState
{
    FULL,
    GOOD,
    OKAY,
    LOW,
    DEAD
}

public class Battery : MonoBehaviour
{
    public static event Action<BatteryState> OnBatteryUpdated;

    [SerializeField] private float batteryTime = 300.0f;

    private WaitForSecondsRealtime waitTime;

    private BatteryState currentState;

    private void Start()
    {
        waitTime = new WaitForSecondsRealtime(batteryTime);

        StartCoroutine(BatteryTimer());
    }

    IEnumerator BatteryTimer()
    {
        yield return waitTime;

        switch (currentState)
        {
            case BatteryState.FULL:
                currentState = BatteryState.GOOD;
                break;

            case BatteryState.GOOD:
                currentState = BatteryState.OKAY;
                break;

            case BatteryState.OKAY:
                currentState = BatteryState.LOW;
                break;

            case BatteryState.LOW:
                currentState = BatteryState.DEAD;
                break;

            case BatteryState.DEAD:
                Debug.Log("GAME OVER!");
                break;
        }

        OnBatteryUpdated(currentState);

        StartCoroutine(BatteryTimer());
    }
}
