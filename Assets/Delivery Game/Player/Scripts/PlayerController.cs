using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;

    private bool _movingForward;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _rb.velocity = transform.forward * movementSpeed * Time.deltaTime * 20f;
            _movingForward = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _rb.velocity = transform.forward * -movementSpeed * Time.deltaTime * 20f;
            _movingForward = false;
        }
        else if (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            _rb.velocity = Vector3.zero;
        }
        if (Input.GetKey(KeyCode.A) && _movingForward)
        {
            transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime * 10f);
        }
        else if (Input.GetKey(KeyCode.A) && !_movingForward)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime * 10f);
        }
        if (Input.GetKey(KeyCode.D) && _movingForward)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime * 10f);
        }
        else if (Input.GetKey(KeyCode.D) && !_movingForward)
        {
            transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime * 10f);
        }
    }
}
