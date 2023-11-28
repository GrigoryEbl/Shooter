using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    private float _damage;

    internal void Initialize(float damage)
    {
        _damage = damage;
    }

    public void Shoot(Vector3 startPoint, Vector3 speed)
    {
        _rigidbody.position = startPoint;
        _rigidbody.velocity = speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider != null)
        {
            var health = collision.collider.GetComponentInParent<AbstractHealth>();

            if(health != null)
            {
                health.TakeDamage(_damage);
            }
        }
    }
}
