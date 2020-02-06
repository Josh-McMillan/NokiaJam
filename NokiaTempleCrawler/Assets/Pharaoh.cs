using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PharaohSpeed
{
    SLOW,
    FAST
}

public class Pharaoh : MonoBehaviour
{
    public static event Action OnCaughtPlayer;

    [SerializeField] private float slow = 1.0f;

    [SerializeField] private float fast = 2.0f;

    private PharaohSpeed speed = PharaohSpeed.SLOW;

    private Transform player = null;

    private bool hasCaughtPlayer = false;

    private bool canMove = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void OnEnable()
    {
        GameManager.OnGameWon += StopMoving;
    }

    private void OnDisable()
    {
        GameManager.OnGameWon -= StopMoving;
    }

    private void Update()
    {
        if (canMove)
        {
            switch (speed)
            {
                case PharaohSpeed.SLOW:
                    transform.position = Vector3.MoveTowards(transform.position, player.position, slow * Time.deltaTime);
                    break;

                case PharaohSpeed.FAST:
                    transform.position = Vector3.MoveTowards(transform.position, player.position, fast * Time.deltaTime);
                    break;
            }
        }
    }

    private void StopMoving()
    {
        canMove = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player has died!");
            canMove = false;
            OnCaughtPlayer();
        }
    }
}
