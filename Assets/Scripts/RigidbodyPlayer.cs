using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyPlayer : MonoBehaviour
{
    [SerializeField] private float _speed = 3;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 playerInput = new Vector3(Input.GetAxis("Horizontal") * _speed, _rigidbody.velocity.y, Input.GetAxis("Vertical") * _speed);

        _rigidbody.velocity = playerInput;
        _rigidbody.velocity += Physics.gravity;
    }
}
