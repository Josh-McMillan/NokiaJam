using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private float rotateSpeed = 3.0f;

    private CharacterController controller;

    private Controls controls = null;

    private void Awake()
    {
        controls = new Controls();
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable() => controls.Player.Enable();

    private void OnDisable() => controls.Player.Disable();

    private void Update()
    {
        Rotate();
        Move();
    }

    private void Move()
    {
        float movement_in = controls.Player.Move.ReadValue<float>();

        Vector3 movement = new Vector3(0.0f, 0.0f, movement_in);

        movement = transform.TransformDirection(movement);

        movement.Normalize();

        controller.SimpleMove(movement * moveSpeed);
    }

    private void Rotate()
    {
        float rotation_in = controls.Player.Rotate.ReadValue<float>();

        Vector3 rotation = new Vector3(0.0f, rotation_in, 0.0f);

        transform.Rotate(rotation * rotateSpeed);
    }

    public void SnapLeft()
    {
        transform.Rotate(0.0f, -45.0f, 0.0f);
    }

    public void SnapRight()
    {
        transform.Rotate(0.0f, 45.0f, 0.0f);
    }
}