using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignalUI : MonoBehaviour
{
    [Header("Signal Sprites")]
    [SerializeField] private Sprite strongReception;
    [SerializeField] private Sprite goodReception;
    [SerializeField] private Sprite okayReception;
    [SerializeField] private Sprite poorReception;
    [SerializeField] private Sprite noReception;

    private Image signalIndicator;

    private void Start()
    {
        signalIndicator = GetComponent<Image>();

        signalIndicator.sprite = strongReception;
    }

    private void OnEnable()
    {
        SignalReception.OnSignalUpdated += UpdateUI;
    }

    private void OnDisable()
    {
        SignalReception.OnSignalUpdated -= UpdateUI;
    }

    private void UpdateUI(SignalStrength strength)
    {
        switch (strength)
        {
            case SignalStrength.STRONG:
                signalIndicator.sprite = strongReception;
                break;

            case SignalStrength.GOOD:
                signalIndicator.sprite = goodReception;
                break;

            case SignalStrength.OKAY:
                signalIndicator.sprite = okayReception;
                break;

            case SignalStrength.POOR:
                signalIndicator.sprite = poorReception;
                break;

            case SignalStrength.NONE:
                signalIndicator.sprite = noReception;
                break;

            default:
                signalIndicator.sprite = strongReception;
                break;
        }
    }
}
