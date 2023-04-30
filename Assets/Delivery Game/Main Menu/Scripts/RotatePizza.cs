using System;
using UnityEngine;

public class RotatePizza : MonoBehaviour
{
    [SerializeField] private float speed;
    
    private void Update()
    {
        transform.Rotate(0f, 0f, speed * Time.deltaTime);
    }
}
