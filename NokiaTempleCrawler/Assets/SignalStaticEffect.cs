using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignalStaticEffect : MonoBehaviour
{
    [Header("Static Strengths")]
    [SerializeField] private float strongReception = 0.025f;
    [SerializeField] private float goodReception = 0.05f;
    [SerializeField] private float okayReception = 0.1f;
    [SerializeField] private float poorReception = 0.3f;
    [SerializeField] private float noReception = 0.75f;

    private RawImage staticImage;

    private void Start()
    {
        staticImage = GetComponent<RawImage>();

        SetShaderValue(strongReception);
    }

    private void OnEnable()
    {
        SignalReception.OnSignalUpdated += SetStaticStrength;
    }

    private void OnDisable()
    {
        SignalReception.OnSignalUpdated -= SetStaticStrength;
    }

    private void SetStaticStrength(SignalStrength strength)
    {
        switch (strength)
        {
            case SignalStrength.STRONG:
                SetShaderValue(strongReception);
                break;

            case SignalStrength.GOOD:
                SetShaderValue(goodReception);
                break;

            case SignalStrength.OKAY:
                SetShaderValue(okayReception);
                break;

            case SignalStrength.POOR:
                SetShaderValue(poorReception);
                break;

            case SignalStrength.NONE:
                SetShaderValue(noReception);
                break;

            default:
                SetShaderValue(strongReception);
                break;
        }
    }

    private void SetShaderValue(float amount)
    {
        staticImage.material.SetFloat("_staticAmount", amount);
    }

}
