using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] GameObject _camera;
    [SerializeField] float _parallaxEffect;

    private float _startPosition, _length;

    private void Start()
    {
        _startPosition = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float distance = _camera.transform.position.x * _parallaxEffect; // 0 = move with camera || 1 = won't move || 0.5 = half
        float movement = _camera.transform.position.x * (1 - _parallaxEffect);

        transform.position = new Vector3(_startPosition + distance, transform.position.y, transform.position.z);

        if (movement > _startPosition + _length)
        {
            _startPosition += _length;
        }
        else if (movement < _startPosition - _length)
        {
            _startPosition -= _length;
        }

    }
}
