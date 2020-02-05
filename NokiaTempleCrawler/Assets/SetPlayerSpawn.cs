using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerSpawn : MonoBehaviour
{
    private Transform player = null;

    private void OnEnable()
    {
        FindPlayer();
        SetPlayerToSpawnPoint();
    }

    private void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void SetPlayerToSpawnPoint()
    {
        player.position = transform.position;
    }
}
