using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _maxDistance;
    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private Projectile _prefab;
    [SerializeField] private float _velocity = 100f;
    [SerializeField] private float _impactForce = 10;
    [SerializeField] private Transform _decal;
    [SerializeField] private float _decalOffset;
    [SerializeField] private ShootEffect _shootEffect;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _reloadSound;

    private Vector3 _startPoint;
    private Vector3 _direction;
    private Collider _collider;

    public void Initialize(CharacterController characterController)
    {
        _collider = characterController as Collider;
    }

    public void Shoot(Vector3 startPoint, Vector3 direction)
    {
        _startPoint = startPoint;
        _direction = direction;
        _shootEffect.Perfom();
        RaycastShoot(startPoint, direction);
        _animator.SetTrigger("Shoot");
        //ProjectileShoot(startPoint, direction * _velocity);
    }

    private void ProjectileShoot(Vector3 startPoint, Vector3 velocity)
    {
        var projectile = Instantiate(_prefab);
        projectile.Initialize(_damage, _collider);

        projectile.Shoot(startPoint, velocity);
    }

    private void RaycastShoot(Vector3 startPoint, Vector3 direction)
    {
        if (Physics.SphereCast(startPoint, 0.01f, direction, out RaycastHit hitInfo, _maxDistance, _layerMask, QueryTriggerInteraction.Ignore))
        {
            var decal = Instantiate(_decal, hitInfo.transform);
            decal.position = hitInfo.point + hitInfo.normal * _decalOffset;
            decal.LookAt(hitInfo.point);
            decal.Rotate(Vector3.up, 180, Space.Self);

            var health = hitInfo.collider.GetComponentInParent<AbstractHealth>();

            if (health != null)
            {
                health.TakeDamage(_damage);
            }

            var victimBody = hitInfo.rigidbody; 

            if (victimBody != null)
            {
                victimBody.AddForceAtPosition(direction * _impactForce, hitInfo.point);
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

    public void ShootSound()
    {

    }
}
