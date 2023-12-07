using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSplasher : MonoBehaviour
{
    [SerializeField] private Transform _splashPrefab;
    private Transform _transform;


    private void Awake()
    {
        _transform = transform;  
    }

    public bool CheckWater(Vector3 input)
    {
        return input.y < _transform.position.y;
    }

    private Vector3 RaycastToVirtualPlane(Vector3 StartPoint, Vector3 direction)
    {
        Plane plane = new Plane(Vector3.up, _transform.position);
        Ray ray = new Ray(StartPoint, direction);

       if( plane.Raycast(ray, out float enter))
        return (StartPoint + direction.normalized * enter);

       return Vector3.zero;
    }

    public void TryCreateWaterSplash(Vector3 StartPoint, Vector3 endPoint)
    {
        if (CheckWater(endPoint))
        {
          var point = RaycastToVirtualPlane(StartPoint, endPoint - StartPoint);
            Destroy(Instantiate(_splashPrefab, point, _splashPrefab.rotation), 5);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
