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

    public static event Action OnBatteryDied;

    public static Action OnBatteryCharge;

    [SerializeField] private float batteryTime = 300.0f;

    private WaitForSecondsRealtime waitTime;

    private BatteryState currentState;

    private void Start()
    {
        waitTime = new WaitForSecondsRealtime(batteryTime);

    }

    private void OnEnable()
    {
        OnBatteryCharge += ChargeBattery;
        GameManager.OnGameStart += StartDraining;
    }

    private void OnDisable()
    {
        OnBatteryCharge -= ChargeBattery;
        GameManager.OnGameStart -= StartDraining;
    }

    private void StartDraining()
    {
        StartCoroutine(BatteryTimer());
    }

    private void ChargeBattery()
    {
        StopCoroutine(BatteryTimer());

        switch (currentState)
        {
            case BatteryState.FULL:
                break;

            case BatteryState.GOOD:
                currentState = BatteryState.FULL;
                break;

            case BatteryState.OKAY:
                currentState = BatteryState.GOOD;
                break;

            case BatteryState.LOW:
                currentState = BatteryState.OKAY;
                break;

            case BatteryState.DEAD:
                currentState = BatteryState.LOW;
                break;
        }

        OnBatteryUpdated(currentState);

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
                OnBatteryDied();
                break;
        }

        OnBatteryUpdated(currentState);

        StartCoroutine(BatteryTimer());
    }
}
