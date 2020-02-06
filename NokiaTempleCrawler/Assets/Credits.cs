using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] private float animationSpeed = 0.1f;

    private RectTransform rect;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (rect.anchoredPosition.y < 144.0f)
        {
            rect.anchoredPosition += new Vector2(0.0f, animationSpeed);
        }
        else
        {
            rect.anchoredPosition = new Vector2(0.0f, 144.0f);
        }
    }
}
