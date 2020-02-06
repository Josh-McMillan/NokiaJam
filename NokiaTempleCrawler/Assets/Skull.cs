using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skull : MonoBehaviour
{
    [SerializeField] private Material skull;

    [SerializeField] private Material victory;

    private Image transitionImage;

    private GameObject credits;

    private void Start()
    {
        transitionImage = GetComponent<Image>();

        transitionImage.material = skull;

        PlayStrike();
    }

    private void OnEnable()
    {
        GameManager.OnArtifactCollect += PlayStrike;
        GameManager.OnPlayerDeath += PlayDeath;
        GameManager.OnGameWon += PlayVictory;
    }

    private void OnDisable()
    {
        GameManager.OnArtifactCollect -= PlayStrike;
        GameManager.OnPlayerDeath -= PlayDeath;
        GameManager.OnGameWon -= PlayVictory;
    }

    private void SetTransparency(float value)
    {
        transitionImage.material.SetFloat("_alphaValue", value);
    }

    private void PlayStrike()
    {
        StopAllCoroutines();
        StartCoroutine(StrikeAnimation());
    }

    private void PlayStrike(PickupType pickupType)
    {
        StopAllCoroutines();
        StartCoroutine(StrikeAnimation());
    }

    private void PlayDeath()
    {
        StopAllCoroutines();
        StartCoroutine(DeathAnimation());
    }

    private void PlayVictory()
    {
        StopAllCoroutines();
        StartCoroutine(VictoryAnimation());
    }

    IEnumerator StrikeAnimation()
    {
        float currentValue = 1.0f;

        SetTransparency(currentValue);

        yield return new WaitForSecondsRealtime(0.5f);

        while (currentValue > 0.0f)
        {
            currentValue -= 0.1f;
            SetTransparency(currentValue);

            yield return new WaitForEndOfFrame();
        }

        currentValue = 0.0f;
        SetTransparency(currentValue);
    }

    IEnumerator DeathAnimation()
    {
        float currentValue = 0.0f;

        SetTransparency(currentValue);

        while (currentValue < 1.0f)
        {
            currentValue += 0.01f;
            SetTransparency(currentValue);

            yield return new WaitForEndOfFrame();
        }

        currentValue = 1.0f;
        SetTransparency(currentValue);

        yield return new WaitForSecondsRealtime(3.0f);

        Debug.Log("Returning to menu!");
    }

    IEnumerator VictoryAnimation()
    {
        transitionImage.material = victory;

        float currentValue = 0.0f;

        SetTransparency(currentValue);

        while (currentValue < 1.0f)
        {
            currentValue += 0.005f;
            SetTransparency(currentValue);

            yield return new WaitForEndOfFrame();
        }

        currentValue = 1.0f;
        SetTransparency(currentValue);

        yield return new WaitForSecondsRealtime(3.0f);

        Debug.Log("Roll credits!");

        credits.SetActive(true);
    }
}
