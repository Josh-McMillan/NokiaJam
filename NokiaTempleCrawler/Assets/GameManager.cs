using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static event Action<PickupType> OnArtifactCollect;
    public static event Action OnPlayerDeath;
    public static event Action OnGameWon;

    public static event Action OnGameStart;

    private bool hasBeetle = false;
    private bool hasCross = false;
    private bool hasScroll = false;

    private void Start()
    {
        OnGameStart();
    }

    private void OnEnable()
    {
        Pickup.OnPickupCollected += ProcessPickup;
        Battery.OnBatteryDied += ProcessDeath;
        Pharaoh.OnCaughtPlayer += ProcessDeath;
    }

    private void OnDisable()
    {
        Pickup.OnPickupCollected -= ProcessPickup;
        Battery.OnBatteryDied -= ProcessDeath;
        Pharaoh.OnCaughtPlayer -= ProcessDeath;
    }

    private void ProcessDeath()
    {
        OnPlayerDeath();
    }

    private void ProcessPickup(PickupType pickup)
    {
        switch (pickup)
        {
            case PickupType.BATTERY:
                Battery.OnBatteryCharge();
                break;

            case PickupType.BEETLE:
                hasBeetle = true;
                break;

            case PickupType.CROSS:
                hasCross = true;
                break;

            case PickupType.SCROLL:
                hasScroll = true;
                break;

            default:
                break;
        }

        if (CheckVictoryCondition())
        {
            OnGameWon();
        }
        else
        {
            OnArtifactCollect(pickup);
        }
    }

    private bool CheckVictoryCondition()
    {
        return hasBeetle && hasCross && hasScroll;
    }
}
