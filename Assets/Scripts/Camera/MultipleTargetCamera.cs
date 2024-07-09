using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultipleTargetCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] List<Transform> _targets;
    [SerializeField] Vector3 _offset;
    [SerializeField] float _smoothTime = .5f;
    [SerializeField] float _smoothZoomTime = 1f;

    [SerializeField] float _minZoom = 40f;
    [SerializeField] float _maxZoom = 10f;
    [SerializeField] float _zoomLimiter = 50f;

    private Vector3 _velocity;
    

    private void LateUpdate()
    {
        if (_targets.Count == 0)
            return;

        Move();
        Zoom();
    }

    private void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + _offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref _velocity, _smoothTime);
    }

    private void Zoom()
    {
        float newZoom = Mathf.Lerp(_maxZoom, _minZoom, GetGreatestDistance() / _zoomLimiter);
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, newZoom, Time.deltaTime * _smoothZoomTime);
    }

    private Vector3 GetCenterPoint()
    {
        if(_targets.Count == 1)
        {
            return _targets[0].position;
        }

        Bounds bounds = new(_targets[0].position, Vector3.zero);
        for (int i = 0; i < _targets.Count; i++)
        {
            bounds.Encapsulate(_targets[i].position);
        }
        
        return bounds.center;
    }

    private float GetGreatestDistance()
    {
        Bounds bounds = new(_targets[0].position, Vector3.zero);

        for (int i = 0; i < _targets.Count; i++)
        {
            bounds.Encapsulate(_targets[i].position);
        }

        return bounds.size.x;

    }


}
