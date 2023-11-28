using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _maxDistance;
    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private Rigidbody _prefab;
    [SerializeField] private float _velocity = 100f;

    private Vector3 _startPoint;
    private Vector3 _direction;

    public void Shoot(Vector3 startPoint, Vector3 direction)
    {
        _startPoint = startPoint;
        _direction = direction;
        //RaycastShoot(startPoint, direction);
        ProjectileShoot(startPoint, direction * _velocity);
    }

    private void ProjectileShoot(Vector3 startPoint, Vector3 direction)
    {

    }

    private void RaycastShoot(Vector3 startPoint, Vector3 direction)
    {
        if (Physics.SphereCast(startPoint, 0.5f, direction, out RaycastHit hitInfo, _maxDistance, _layerMask, QueryTriggerInteraction.Ignore))
        {
            var health = hitInfo.collider.GetComponentInParent<AbstractHealth>();

            if (health != null)
            {
                health.TakeDamage(_damage);
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        if (Physics.Raycast(_startPoint, _direction, out RaycastHit hitInfo, _maxDistance, _layerMask, QueryTriggerInteraction.Ignore))
        {
            Gizmos.DrawLine(_startPoint, hitInfo.point);
            Gizmos.DrawSphere(hitInfo.point, 0.1f);
        }
    }
}
