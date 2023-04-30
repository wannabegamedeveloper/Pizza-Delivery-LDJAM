using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarAI : MonoBehaviour
{
    [SerializeField] private float speed;

    private Transform _ai;
    private Transform _model;
    private Vector3 _oldPos;
    private Quaternion _oldRotation;
    private bool _turned;

    private void Start()
    {
        _model = transform;
        _ai = transform.parent.GetChild(1);
        _oldPos = _ai.position;
    }

    private void Update()
    {
        _ai.Translate(0f, -Time.deltaTime * speed * 5f, 0f);
        _model.position = Vector3.Lerp(_model.position, _ai.position, 10f * Time.deltaTime);
        _model.localRotation = Quaternion.Lerp(_model.localRotation, _ai.localRotation, 10f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Turn") && !_turned)
        {
            int x = Random.Range(0, 3);
            if (x == 0)
            {
                _turned = true;
                _ai.Rotate(0f, 0f, -90f);
            }
            else if (x == 1)
            {
                _turned = true;
                _ai.Rotate(0f, 0f, 90f);
            }
        }
        else if (other.CompareTag("Edges"))
        {
            _model.position = _oldPos;
            _ai.position = _oldPos;
            _model.rotation = _oldRotation;
            _ai.rotation = _oldRotation;
            _turned = false;
        }
    }
}
