using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactRoom : MonoBehaviour
{
    [SerializeField] private GameObject beetle;

    [SerializeField] private GameObject cross;

    [SerializeField] private GameObject scroll;

    [SerializeField] private GameObject mummy;

    private bool hasSpawnedMummy = false;

    private void OnEnable()
    {
        GameManager.OnArtifactCollect += ShowArtifact;
    }

    private void OnDisable()
    {
        GameManager.OnArtifactCollect -= ShowArtifact;
    }

    private void ShowArtifact(PickupType artifactType)
    {
        switch (artifactType)
        {
            case PickupType.BEETLE:
                beetle.SetActive(true);
                break;

            case PickupType.CROSS:
                cross.SetActive(true);
                break;

            case PickupType.SCROLL:
                scroll.SetActive(true);
                break;

            case PickupType.BATTERY:
                break;

            default:
                Debug.Log("Something went wrong showing the artifact!");
                break;
        }

        if (!hasSpawnedMummy)
        {
            hasSpawnedMummy = true;
            Instantiate(mummy, transform.position, mummy.transform.rotation);
        }
    }
}
