using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;

    private float _movement;
    private bool _movingForward;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rb.velocity = transform.forward * _movement * Time.deltaTime * 20f;

        if (_rb.velocity != Vector3.zero)
            _movingForward = true;
        
        if (Input.GetKey(KeyCode.W))
        {
            if (_movement < movementSpeed)
                _movement += 50f * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //_rb.velocity = transform.forward * -movementSpeed * Time.deltaTime * 20f;
            //_movingForward = false;
        }
        else if (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            if (_movement > 0f)
                _movement--;
        }
        if (Input.GetKey(KeyCode.A) && _movingForward)
        {
            transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime * 10f);
        }
        if (Input.GetKey(KeyCode.D) && _movingForward)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime * 10f);
        }
    }
}
