using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(ObjectPoolManager))]
public class Bow : Sounds
{
    [SerializeField] private ObjectPoolManager _pool;
    [SerializeField] private Transform _bowPosition;
    [SerializeField] private Transform _handPosition;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _arrowSpeed = 12f;
    [Header("Trajectory projection")]
    [SerializeField] private GameObject _point;
    [SerializeField] private Transform _pointsContainer;
    [SerializeField] private int _numberOfPoints;
    [SerializeField] private float _spaceBetweenPoints;
    [Space(10)]
    [Header("Hidden elements")]
    [SerializeField] private GameObject[] _elementsAiming;
    [SerializeField] private GameObject[] _defaultElements;
    [Space(10)]
    [Header("Massage")]
    [SerializeField] private GameObject _massageBox;
    [SerializeField] private float _showTimeMassage;
    [Space(10)]
    [Header("Sound Settings")]
    [SerializeField] private float _volume = 1f;

    private GameObject[] _points;

    private float _rotateDirection = 0;
    private bool _isAiming;

    private void Awake()
    {
        _massageBox.SetActive(false);
        if (_pool ==  null)
        {
            _pool = GetComponent<ObjectPoolManager>();
        }
    }

    private void Start()
    {
        _points = new GameObject[_numberOfPoints];
        for (int i = 0; i < _numberOfPoints; i++)
        {
            _points[i] = Instantiate(_point, _bowPosition.position, Quaternion.identity, _pointsContainer);
        }
        OnDefaultState();
    }

    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetKey(KeyCode.K) && _playerMovement.CheckGrounding())
            {
                OnAiming();
                PlayerInput();
                _isAiming = true;
                _playerMovement.SetAiming(_isAiming);
                for (int i = 0; i < _numberOfPoints; i++)
                {
                    _points[i].SetActive(true);
                    _points[i].transform.position = PointPosition(i * _spaceBetweenPoints);
                }
            }
            else
            {
                OnDefaultState();
                _isAiming = false;
                _playerMovement.SetAiming(_isAiming);
                SetDefaultPosition();
                for (int i = 0; i < _numberOfPoints; i++)
                {
                    _points[i].SetActive(false);
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (_massageBox.activeSelf)
            _massageBox.transform.rotation = Quaternion.identity;
    }

    private void FixedUpdate()
    {
        if(_isAiming)
        {
            RoteteHand();
        }
    }


    private Vector2 PointPosition(float timeAfterShot)
    {
        Vector2 initialPosition = _bowPosition.position;
        Vector2 initialVelocity = _bowPosition.right * _arrowSpeed;

        Vector2 position = initialPosition + initialVelocity * timeAfterShot + 0.5f * timeAfterShot * timeAfterShot * Physics2D.gravity;
        return position;
    }

    private void PlayerInput()
    {
        _rotateDirection = Input.GetAxisRaw("Vertical One");

        if (Input.GetKeyUp(KeyCode.H))
        {
            Shot();
        }
    }
    private void Shot()
    {
        ObjectPool arrow = _pool.GetFreeElement(_bowPosition.position, _bowPosition.rotation);
        if (arrow == null)
        {
            StartCoroutine(nameof(ShowMassage));
            return;
        }
        PlaySound(0, _volume);
        Rigidbody2D arrowRb2D = arrow.GetRigidBody2D;
        arrowRb2D.linearVelocity = arrowRb2D.transform.right * _arrowSpeed;
    }

    IEnumerator ShowMassage()
    {
        _massageBox.SetActive(true);
        yield return new WaitForSeconds(_showTimeMassage);
        _massageBox.SetActive(false);
    }

    private void RoteteHand()
    {
        _handPosition.transform.Rotate(0.0f, 0.0f, _rotationSpeed * _rotateDirection, Space.Self);
    }

    private void SetDefaultPosition()
    {
        _handPosition.localEulerAngles = new Vector3(_handPosition.localEulerAngles.x, _handPosition.localEulerAngles.y, 0);
    }

    private void OnAiming()
    {
        foreach (var item in _elementsAiming)
        {
            item.SetActive(true);
        }
        foreach (var item in _defaultElements)
        {
            item.SetActive(false);
        }
    }

    private void OnDefaultState()
    {
        foreach (var item in _elementsAiming)
        {
            item.SetActive(false);
        }
        foreach (var item in _defaultElements)
        {
            item.SetActive(true);
        }
    }
}
