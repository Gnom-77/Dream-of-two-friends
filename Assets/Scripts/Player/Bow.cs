using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(ObjectPoolManager))]
public class Bow : MonoBehaviour
{
    [SerializeField] Transform _bowPosition;
    [SerializeField] Transform _handPosition;
    [SerializeField] PlayerMovement _playerMovement;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _arrowSpeed = 12f;

    private ObjectPoolManager _pool;
    private float _rotateDirection = 0;
    private bool _isAiming;

    private void Start()
    {
        _pool = GetComponent<ObjectPoolManager>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
            PlayerInput();
            _isAiming = true;
            _playerMovement.SetAiming(_isAiming);
        }
        if (!Input.GetKey(KeyCode.K))
        {
            _isAiming = false;
            _playerMovement.SetAiming(_isAiming);
            SetDefaultPosition();
        }
    }

    private void FixedUpdate()
    {
        if(_isAiming)
        {
            RoteteHand();
        }
    }

    private void Shot()
    {
        ObjectPool arrow = _pool.GetFreeElement(_bowPosition.position, _bowPosition.rotation);
        Rigidbody2D arrowRb2D = arrow.GetRigidBody2D;
        arrowRb2D.linearVelocity = arrowRb2D.transform.right * _arrowSpeed;
    }

    private void PlayerInput()
    {
        _rotateDirection = Input.GetAxisRaw("Vertical One");

        if (Input.GetKeyUp(KeyCode.H))
        {
            Shot();
        }
    }
    private void RoteteHand()
    {
        _handPosition.transform.Rotate(0.0f, 0.0f, _rotationSpeed * _rotateDirection, Space.Self);
    }

    private void SetDefaultPosition()
    {
        _handPosition.localEulerAngles = new Vector3(_handPosition.localEulerAngles.x, _handPosition.localEulerAngles.y, 0);
    }
}
