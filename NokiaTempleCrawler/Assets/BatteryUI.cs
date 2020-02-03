using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryUI : MonoBehaviour
{
    [Header("Signal Sprites")]
    [SerializeField] private Sprite fullBattery;
    [SerializeField] private Sprite goodBattery;
    [SerializeField] private Sprite okayBattery;
    [SerializeField] private Sprite lowBattery;
    [SerializeField] private Sprite deadBattery;

    private Image batteryIndicator;

    private void Start()
    {
        batteryIndicator = GetComponent<Image>();

        batteryIndicator.sprite = fullBattery;
    }

    private void OnEnable()
    {
        Battery.OnBatteryUpdated += UpdateUI;
    }

    private void OnDisable()
    {
        Battery.OnBatteryUpdated -= UpdateUI;
    }

    private void UpdateUI(BatteryState strength)
    {
        switch (strength)
        {
            case BatteryState.FULL:
                batteryIndicator.sprite = fullBattery;
                break;

            case BatteryState.GOOD:
                batteryIndicator.sprite = goodBattery;
                break;

            case BatteryState.OKAY:
                batteryIndicator.sprite = okayBattery;
                break;

            case BatteryState.LOW:
                batteryIndicator.sprite = lowBattery;
                break;

            case BatteryState.DEAD:
                batteryIndicator.sprite = deadBattery;
                break;

            default:
                batteryIndicator.sprite = fullBattery;
                break;
        }
    }
}
