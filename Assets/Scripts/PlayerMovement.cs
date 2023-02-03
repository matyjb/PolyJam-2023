using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    private float speed;

    [SerializeField]
    private float rotationSpeed;

    float _horizontalInput;
    float _verticalInput;
    Rigidbody2D _rb2d;

    private void Awake() {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    void Update() {
        GetPlayerInput();
    }

    private void FixedUpdate() {
        //MovePlayer();
        _rb2d.velocity = transform.right * speed;
        RotatePlayer();
    }

    void GetPlayerInput() {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }

    void MovePlayer() {
        _rb2d.velocity = transform.right * Mathf.Clamp01(_verticalInput) * speed;
    }

    void RotatePlayer() {
        float rotation = -_horizontalInput * rotationSpeed;
        transform.Rotate(Vector3.forward * rotation);
    }
}